using Godot;
using System;

/// <summary>
/// in the idle state we dont want to do anything atm.
/// 
/// </summary>
public class EnemyDirectionStateRight: EnemyDirectionState{


    public Path2D Path;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="enemy"></param>
    public EnemyDirectionStateRight(OctoEnemy enemy) : base(enemy){

    }

    public override void _PhysicsProcess(float delta){
        if(parent.LinearVelocity.x < 0){
            parent.setDirection(new EnemyDirectionStateLeft(parent));
        }
    }
    public override void enter(){
        view_area.Scale = new Vector2(-1, view_area.Scale.y);
        sprite.Scale = new Vector2(-Mathf.Abs(sprite.Scale.x),sprite.Scale.y);
        sprite_shoot.Scale = new Vector2(-Mathf.Abs(sprite_shoot.Scale.x),sprite_shoot.Scale.y);
        laser.Position = new Vector2(Mathf.Abs(laser.Position.x),laser.Position.y);
    }
    public override void exit(){
        GD.Print("exit dir right;");
    }




}