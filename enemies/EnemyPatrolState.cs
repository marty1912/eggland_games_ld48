using Godot;
using System;

public class EnemyPatrolState : EnemyState{


    public Path2D path;
    public Vector2[] patrol_points;
    public int patrol_index;
    public float distance_tolerance = 10;

    public EnemyPatrolState(OctoEnemy enemy) : base(enemy){
        this.path = this.parent.patrolpath;
        this.patrol_points = this.path.Curve.GetBakedPoints();
    }

/// <summary>
/// returns the distance to the patrol point with index point_index
/// </summary>
/// <param name="point_index"></param>
/// <returns></returns>
public float distanceToPoint(int point_index){
        Vector2 my_pos = path.ToLocal(parent.GlobalPosition);
        Vector2 cur_point = patrol_points[point_index];
        return (cur_point - my_pos).Length();

}
public void CheckBoatInArea(){
        Godot.Collections.Array bodies = view_area.GetOverlappingBodies();
        for (int i = 0; i < bodies.Count;i++){
            Node2D body =(Node2D) bodies[i];

            if(body is Boat){
                this.parent.setNextState(new EnemyAlarmState(parent, (Boat) body));
            }
        }
    }
   public override void _PhysicsProcess(float delta){
        if(distanceToPoint(patrol_index) < distance_tolerance){
            patrol_index++;
            patrol_index %= patrol_points.Length;
        }
        Vector2 target = patrol_points[patrol_index];
        float movementForce = Mathf.Clamp(distanceToPoint(patrol_index), 0, parent.maxMovementForce);
        parent.AppliedForce = (target - path.ToLocal(parent.GlobalPosition)).Normalized() * movementForce;
        CheckBoatInArea();

    }

    /// <summary>
    /// finds the closest point on the patrol path. and returns the index in the patrol_points array;
    /// </summary>
    /// <returns></returns>
    public int getClosestPointIndex(){
        float min_dist = Mathf.Inf;
        int min_index = -1;
        for (int i = 0; i < patrol_points.Length;i++){
            float distance = distanceToPoint(i);
            if(distance < min_dist){
                min_dist = distance;
                min_index = i;
            }
        }
        return min_index;
    }
    public override void enter(){
        GD.Print("enter patrol state");
        patrol_index = getClosestPointIndex();
        light.Color = new Color("f481f0");
    }
    public override void exit(){
        GD.Print("exit patrol state");
    }

}