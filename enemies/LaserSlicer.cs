using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;

public class LaserSlicer : Slicer2D 
{
    /// <summary>
    /// hardcoded value to make sure we slice through the boat
    /// </summary>
    public float slice_dist = 1000;

    public override void _Ready()
    {
        
    }

    /// <summary>
    /// gets the slicing data for the given boat. might be 0 if we do not hit it.
    /// </summary>
    /// <param name="boat"></param>
    /// <param name="start"></param>
    /// <returns></returns>
    public List<SlicingData> getBoatSlicingData(Boat boat, Vector2 start) {

        Vector2 end = this.ToLocal(boat.GlobalPosition);
        List<SlicingData> dat = sliceWorld(GetGlobalTransform().Xform(start), GetGlobalTransform().Xform(end));
        return dat;
    }

    public void sliceBoat(Boat boat,Vector2 start){
        //Vector2 direction = (boat.GlobalPosition - GetGlobalTransform().Xform(start)).Normalized();
        //float len = (boat.GlobalPosition - start).Length();
        List<SlicingData> dat = getBoatSlicingData(boat, start);

        GD.Print("now starting to slice:", dat.Count);
        foreach(SlicingData obj in dat){
        GD.Print("now to slicing:", obj);
            SliceableObject2D[] half = slice(obj);
            half[0].ApplyCentralImpulse(-(obj.global_enter - obj.global_exit).Normalized()* 100);
            half[1].ApplyCentralImpulse((obj.global_enter - obj.global_exit).Normalized()* 100);

        }

    }

}
