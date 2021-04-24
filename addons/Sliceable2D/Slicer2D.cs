using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

[Tool]
public class Slicer2D : Node2D
{


        
        /// <summary>
        /// this class is used to hold the data we need for slicing stuff. 
        /// </summary>
    public class SlicingData
    {
        public SliceableObject2D obj;
        public Vector2 global_enter;
        public Vector2 enter_normal;
        public Vector2 global_exit;
        public int collider_id;
        public Vector2 exit_normal;
        override public String ToString(){
            return this.obj + " enter:" + this.global_enter +" normal:" + this.enter_normal+ " exit:" + this.global_exit+ " normal:" + this.exit_normal;
        }
    }




    /// <summary>
    /// slices all the SliceableObject2Ds that are on the line between start and end.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="collision_layer"></param>
    /// <returns></returns>
    public List<SlicingData> sliceWorld(Vector2 start, Vector2 end, uint collision_layer = 0x7FFFFFFF)
    {
        var data = getObjectsToSlice(start, end, collision_layer);
        foreach(var dat in data){
            slice(dat);
        }

        return new List<SlicingData>();
    }


/// <summary>
/// checks on which side of the line we can find the specified point. returns 1 for on line or on first side or -1 for other side
/// copied from stackoverflow: https://stackoverflow.com/questions/1560492/how-to-tell-whether-a-point-is-to-the-right-or-left-side-of-a-line
/// </summary>
/// <param name="line_start"></param>
/// <param name="line_end"></param>
/// <param name="point_to_check"></param>
public int isPointOnSideOfLine(Vector2 line_start,Vector2 line_end,Vector2 point_to_check){
        return Math.Sign((line_end.x - line_start.x) * (point_to_check.y - line_start.y) - (line_end.y - line_start.y) * (point_to_check.x - line_start.x));
    }

/// <summary>
/// gets the points of a shape
/// </summary>
/// <param name="shape"></param>
/// <returns></returns>
public Vector2[] getShapePoints(Shape2D shape){

        if(shape is ConvexPolygonShape2D){
            return ((ConvexPolygonShape2D)shape).Points;
        }
        else if(shape is RectangleShape2D){
            Vector2[] points = new Vector2[4];
        
            Vector2 ext = ((RectangleShape2D)shape).Extents;
            points[0] = new Vector2(-ext.x, -ext.y);
            points[1] = new Vector2(ext.x, -ext.y);
            points[2] = new Vector2(ext.x, ext.y);
            points[3] = new Vector2(-ext.x, ext.y);
            return points;

        }
        // other shapes are not yet implemented..
        return null;

    }
/// <summary>
/// slices one object
/// </summary>
/// <param name="to_slice"></param>
/// <returns></returns>
        public int slice(SlicingData to_slice){
        GD.Print("slice(",to_slice,")");
        SliceableObject2D obj = to_slice.obj;
        Vector2 local_enter = obj.ToLocal(to_slice.global_enter);
        Vector2 local_exit = obj.ToLocal(to_slice.global_exit);
        // the shape owners are the objects that hold the shapes.
        Godot.Collections.Array shapeOwners = obj.GetShapeOwners();

        List<Shape2D> child1Shapes = new List<Shape2D>();
        List<Shape2D> child2Shapes = new List<Shape2D>();
        for(int shape_owner_index = 0;shape_owner_index < shapeOwners.Count;shape_owner_index++ ){
            uint shapeOwner_id = (uint)(int) shapeOwners[shape_owner_index];
            // skip disabled shapes.
            if(obj.IsShapeOwnerDisabled( shapeOwner_id)){
                continue;
                };
            for (int shape_id = 0; shape_id < obj.ShapeOwnerGetShapeCount( shapeOwner_id);shape_id++)
            {
                List<Vector2> child1Points = new List<Vector2>();
                List<Vector2> child2Points = new List<Vector2>();
                Shape2D shape = obj.ShapeOwnerGetShape( shapeOwner_id, shape_id);
                var points = getShapePoints(shape);
                GD.Print("points",points);
                // shape points could not be extracted..
                if(points == null){
                    continue;
                }
                for (int point_index = 0; point_index < points.Length;point_index++){
                    Vector2 point = points[point_index];
                    if(isPointOnSideOfLine(local_enter,local_exit,point) >0){
                        child1Points.Add(point);
                    }
                    else{
                        child2Points.Add(point);
                    }
                }
                child1Points.Add(local_enter);
                child1Points.Add(local_exit);
                child2Points.Add(local_enter);
                child2Points.Add(local_exit);

                var child1Shape = new ConvexPolygonShape2D();
                child1Shape.Points = child1Points.ToArray();
                foreach(var p in child1Points){
                        GD.Print("child1: ", p);
                }
                child1Shapes.Add(child1Shape);

                var child2Shape = new ConvexPolygonShape2D();
                child2Shape.Points = child2Points.ToArray();
                foreach(var p in child2Points){
                        GD.Print("child2: ", p);
                }
                child2Shapes.Add(child2Shape);


            }

        }
        GD.Print("now creating children..");
        var child1 = createChild(obj, child1Shapes.ToArray());
        var child2 = createChild(obj, child2Shapes.ToArray());
        obj.QueueFree();

        return 0;
    }
    public SliceableObject2D createChild(SliceableObject2D original_object,Shape2D[] shapes){

        GD.Print("now creating child");
        SliceableObject2D child = (SliceableObject2D) original_object.Duplicate();

        Godot.Collections.Array shapeOwners = child.GetShapeOwners();
        for (int shape_owner_index = 0; shape_owner_index < shapeOwners.Count; shape_owner_index++)
        {
            uint shapeOwner_id = (uint)(int)shapeOwners[shape_owner_index];
            // skip disabled shapes.
            child.ShapeOwnerClearShapes(shapeOwner_id);
            child.RemoveShapeOwner(shapeOwner_id);
        }
        MeshInstance2D mesh = new MeshInstance2D();


        GD.Print("child.shapeowners:", child.GetShapeOwners());
        GD.Print("will now get sprite..",child.sprite);
        mesh.Name = child.sprite;
        MeshInstance2D spritenode = (MeshInstance2D) child.GetNode(child.sprite);
        Sprite spritenode2 = (Sprite) child.GetNode(child.sprite2);
        //mesh.Name = spritenode.Name;
        var sprite_rect = spritenode2.GetRect();
        //var sprite_orig = new Vector2(0, 20);//child.ToLocal(spritenode2.GetGlobalTransform().origin);
        var sprite_orig = child.GetGlobalTransform().BasisXformInv(spritenode2.GlobalPosition);
        var sprite_pos =  sprite_orig + sprite_rect.Position;
        var sprite_size =  sprite_rect.Size;

        spritenode.ReplaceBy(mesh);

        //child.AddChild(mesh);
        var shape_owner = child.CreateShapeOwner(child);
        List<Vector2> all_points = new List<Vector2>();
        for (int shape_index = 0; shape_index < shapes.Length;shape_index++){
            child.ShapeOwnerAddShape(shape_owner, shapes[shape_index]);
            foreach(Vector2 point in getShapePoints(shapes[shape_index])){
                all_points.Add(point);
            }
        }
        GD.Print("now setting up the sprite");
        // setup sprite
        var outlines = Geometry.ConvexHull2d(all_points.ToArray());
        var indices = Geometry.TriangulatePolygon(outlines);

        var uvs = new Vector2[outlines.Length];
        for (int uv_index = 0; uv_index < uvs.Length;uv_index++){
            Vector2 vec = (outlines[uv_index] - sprite_pos)/sprite_size;
            GD.Print("outline:",outlines[uv_index],"uvs:", vec, "size",sprite_size,"origin:",sprite_pos);
            vec.x = Mathf.Clamp(vec.x, 0, 1);
            vec.y = Mathf.Clamp(vec.y, 0, 1);
            uvs[uv_index] = vec;
        }

        var arr = new Godot.Collections.Array();
        arr.Resize((int)ArrayMesh.ArrayType.Max);

        arr[(int)Mesh.ArrayType.Vertex] = outlines;
        arr[(int)Mesh.ArrayType.Index] = indices;
        arr[(int)Mesh.ArrayType.TexUv] = uvs;

        var arr_mesh = new ArrayMesh();
        arr_mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles,arr);
        mesh.Mesh = arr_mesh;

        mesh.Texture = (Texture) spritenode2.Texture.Duplicate();

        // resize the collision polygon so the center of mass etc are fixed.
        CollisionPolygon2D collision_node = (CollisionPolygon2D) child.GetNode(child.collisionPoly);
        collision_node.Polygon = outlines;

        original_object.GetParent().AddChild(child);

        //spritenode = (Mesh) child.GetNode(child.sprite);
        //GD.Print("created child.. will now get sprite..",spritenode);


        return child;

    }
    /// <summary>
    /// gets all objects that the line intersects and returns them with entry points
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="collision_layer"></param>
    /// <returns></returns>
    private Godot.Collections.Array castLine(Vector2 start, Vector2 end, uint collision_layer = 0x7FFFFFFF)
    {
        var space_state = GetWorld2d().DirectSpaceState;
        var found_colliders = new Godot.Collections.Array();
        var found_intersects = new Godot.Collections.Array();
        GD.Print("intersecting line..");
        while (true)
        {

            var found_intersecting = space_state.IntersectRay(start, end, found_colliders, collision_layer);
            if (!found_intersecting.Contains("collider"))
            {
                // nothing found
                break;
            }

            found_colliders.Add(found_intersecting["collider"]);
            found_intersects.Add(found_intersecting);

        }

        return found_intersects;
    }

    /// <summary>
    /// finds all the objects that are on the line from start to end and returns them with the entry and exit points
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="collision_layer"></param>
    /// <returns></returns>
    private List<SlicingData> getObjectsToSlice(Vector2 start, Vector2 end, uint collision_layer = 0x7FFFFFFF)
    {

        Godot.Collections.Array forward = castLine(start, end, collision_layer);
        Godot.Collections.Array backward = castLine(end, start, collision_layer);

        List<SlicingData> objects = new List<SlicingData>();

        for (int i = 0; i < forward.Count; i++)
        {
            Dictionary current_intersect = (Dictionary)forward[i];

            if (!(current_intersect["collider"] is SliceableObject2D))
            {
                continue;
            }

            var data = new SlicingData();
            data.obj = (SliceableObject2D)current_intersect["collider"];
            data.collider_id= (int) current_intersect["collider_id"];
            data.global_enter = (Vector2)current_intersect["position"];
            data.enter_normal = (Vector2)current_intersect["normal"];
            objects.Add(data);
        }

        for (int i = 0; i < backward.Count; i++)
        {
            Dictionary current_intersect = (Dictionary)backward[i];

            if (!(current_intersect["collider"] is SliceableObject2D))
            {
                continue;
            }
            // we check if the object is sliced in both directions (going fully through.)
            // if we dont find it in the list it must mean that the object was only sliced 
            // half through in which case we dont want to slice it.
            foreach (var obj in objects)
            {
                if (obj.collider_id == (int) current_intersect["collider_id"])
                {
                    obj.global_exit = (Vector2)current_intersect["position"];
                    obj.exit_normal = (Vector2)current_intersect["normal"];
                    break;
                }
            }
        }

        // in here we check if an object was sliced completely. if not we have no valid normal..
        var found = true;
        while (found)
        {
            found = false;
            foreach (var obj in objects)
            {
                if (obj.exit_normal.Length() == 0 || obj.enter_normal.Length() == 0)
                {
                    objects.Remove(obj);
                    found = true;
                    break;
                }

            }
        }

        return objects;
    }

}