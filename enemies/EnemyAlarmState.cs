using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;

public class EnemyAlarmState : EnemyState{


    public Boat player_boat;
        float alert_time = 0.5f;
    public EnemyAlarmState(OctoEnemy enemy,Boat boat) : base(enemy){
        player_boat = boat;
    }

   public override void _PhysicsProcess(float delta){
        // check if we can reach the ship.
        if((parent.GlobalPosition - player_boat.GlobalPosition).Length() > parent.lineOfSightDistance || (!haveLineOfSight(player_boat) )){

            GD.Print("will exit alarm state",haveLineOfSight(player_boat));
            parent.setNextState(new EnemyPatrolState(parent));
        }
        alert_time -= delta;

        if(alert_time < 0){
            parent.setNextState(new EnemyAimState(parent,player_boat));
        }
    }

    public override void enter(){
        GD.Print("enter alarm state");
        light.Color = new Color("ffffff");
        // start red blinking light.
        tween.StopAll();
        tween.InterpolateProperty(light, "color", new Color("ff0000") , new Color("333333"), 1f);
        tween.Start();
    }
    public override void exit(){

        GD.Print("exit alarm state");
        tween.StopAll();
    }

}