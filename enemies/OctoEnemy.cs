using Godot;
using System;

public class OctoEnemy : RigidBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    public float maxMovementForce = 20;

    [Export]
    public float lineOfSightDistance = 500;
    public Path2D patrolpath; 
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        patrolpath = (Path2D) GetParent();
        this.setNextState(new EnemyPatrolState(this));
        this.setDirection(new EnemyDirectionStateLeft(this));

    }
    EnemyState current_state;
    /// <summary>
    /// state machine..
    /// </summary>
    /// <param name="next_state"></param>
    public void setNextState(EnemyState next_state){
        if(current_state != null){
            current_state.exit();
        }
        current_state = next_state;
        current_state.enter();
    }

    EnemyDirectionState current_dir;
    /// <summary>
    /// state machine..
    /// </summary>
    /// <param name="next_state"></param>
    public void setDirection(EnemyDirectionState next_state){
        if(current_dir != null){
            current_dir.exit();
        }
        current_dir = next_state;
        current_dir.enter();
    }

/// <summary>
/// in here we let our states do their thing. for example follow a path.
/// </summary>
/// <param name="delta"></param>
    public override void _PhysicsProcess(float delta){

        current_state._PhysicsProcess(delta);
        current_dir._PhysicsProcess(delta);

    }


}

