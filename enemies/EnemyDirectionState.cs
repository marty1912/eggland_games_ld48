using System;
using Godot;


public abstract class EnemyDirectionState{

    public Area2D view_area;
    public OctoEnemy parent;
    public EnemyDirectionState(OctoEnemy enemy){
        this.parent = enemy;
        view_area = (Area2D) enemy.GetNode("ViewArea");
    }
    public abstract void enter();
    public abstract void exit();
    public abstract void _PhysicsProcess(float delta);

}