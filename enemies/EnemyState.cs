using System;
using Godot;
using System.Collections.Generic;
using Godot.Collections;


public abstract class EnemyState{

    public Area2D view_area;
    public Light2D light;
    public Tween tween;
    public EnemyLaser laser;
    public LaserSlicer slicer;
    public OctoEnemy parent;
    public EnemyState(OctoEnemy enemy){
        this.parent = enemy;
        this.view_area = (Area2D)parent.GetNode("ViewArea");
        this.light= (Light2D)view_area.GetNode("Light2D");
        this.tween= (Tween)parent.GetNode("Tween");
        this.laser = (EnemyLaser ) parent.GetNode("AimLaser");
        this.slicer = (LaserSlicer) laser.GetNode("Slicer");
    }
    public abstract void enter();
    public abstract void exit();
    public abstract void _PhysicsProcess(float delta);
    public Boolean haveLineOfSight(Node2D boat){


        Physics2DDirectSpaceState space = parent.GetWorld2d().DirectSpaceState;
        Godot.Collections.Array exclude = new Godot.Collections.Array();
        exclude.Add(parent);
        Dictionary res = space.IntersectRay(parent.GlobalPosition,boat.GlobalPosition, exclude);

        if (res.Count == 0)
        {
            return false;
        }
        else if(res["collider"] == boat){
            return true;
        }

        return false;

    }

}