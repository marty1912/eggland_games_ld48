using Godot;
using System;

/// <summary>
/// in the idle state we dont want to do anything atm.
/// 
/// </summary>
public class EnemyIdleState : EnemyState{


    public float idle_time = 3f;
    public Path2D Path;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="enemy"></param>
    public EnemyIdleState(OctoEnemy enemy) : base(enemy){

    }

    public override void _PhysicsProcess(float delta){
        idle_time -= delta;
        if(idle_time < 0){
            parent.setNextState(new EnemyPatrolState(parent));
        }
    }
    public override void enter(){
    }
    public override void exit(){
    }




}