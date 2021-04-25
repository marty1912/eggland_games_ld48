using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;

public class EnemyShootState : EnemyState{

    public Boat player_boat;
    float shoot_time = 1f;
    public EnemyShootState(OctoEnemy enemy,Boat boat) : base(enemy){
        player_boat = boat;
    }

    
    public override void _PhysicsProcess(float delta){
        // check if we can reach the ship.
        shoot_time -= delta;
        laser.CastTo = laser.ToLocal(player_boat.GlobalPosition);
        if(shoot_time < 0){
            parent.setNextState(new EnemyIdleState(parent));
        }
    }

/// <summary>
/// function to shoot a boat.
/// </summary>
/// <param name="boat"></param>
public void shootBoat(Boat boat){
        if (boat.shields == 0)
        {
            slicer.sliceBoat(player_boat, Vector2.Zero);
        }
        else{
            boat.removeShield();
        }
    }
    public override void enter(){
        GD.Print("enter shoot state.");
        laser.Visible = true;
        laser.line.Visible = true;
        // start red blinking light.
        laser.line.Modulate = new Color("ff0000");
        laser.end_particles.Visible = true;
        laser.start_particles.Visible = true;
        laser.startShooting(0.1f);
        tween.StopAll();
        shootBoat(player_boat);
    }
    public override void exit(){
        GD.Print("exit shoot state.");
        laser.stopTweening();
        laser.Visible = false;
        tween.StopAll();
    }

}