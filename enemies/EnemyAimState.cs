using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;

public class EnemyAimState : EnemyState{

    public Boat player_boat;
    float aim_time = 2.5f;
    public EnemyAimState(OctoEnemy enemy,Boat boat) : base(enemy){
        player_boat = boat;
    }

    
    public override void _PhysicsProcess(float delta){
        // check if we can reach the ship.
        if((parent.GlobalPosition - player_boat.GlobalPosition).Length() > parent.lineOfSightDistance || (!haveLineOfSight(player_boat) )){
            parent.setNextState(new EnemyPatrolState(parent));
        }

        aim_time -= delta;
        laser.CastTo = laser.ToLocal(player_boat.GlobalPosition);

        blink_time -= delta;
        if(blink_time < 0){
            blink_time = getCurrentBlinkInterval();
            laser.line.Visible = !laser.line.Visible;
        }
        if(aim_time < 0){
        }
    }
    public float getCurrentBlinkInterval(){
        return blink_interval;
    }

    public Boolean blinking = false;
    public float blink_time = 0.5f;
    public float blink_interval= 0.125f;
    public override void enter(){
        GD.Print("enter aim state.");
        laser.Visible = true;
        laser.line.Visible = true;
        // start red blinking light.
        laser.startAiming(aim_time);
        laser.line.Modulate = new Color("ff0000");
        laser.end_particles.Visible = false;
        laser.start_particles.Visible = false;
        tween.StopAll();
    }
    public override void exit(){
        GD.Print("exit aim state.");
        laser.stopTweening();
        laser.Visible = false;
        tween.StopAll();
    }

}