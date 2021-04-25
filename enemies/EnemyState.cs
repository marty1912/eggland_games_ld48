using System;
using Godot;


public abstract class EnemyState{

    public OctoEnemy parent;
    public EnemyState(OctoEnemy enemy){
        this.parent = enemy;
    }
    public abstract void enter();
    public abstract void exit();
    public abstract void _PhysicsProcess(float delta);

}