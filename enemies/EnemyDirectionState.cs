using System;
using Godot;


public abstract class EnemyDirectionState{

    public Area2D view_area;
    public Sprite sprite;
    public EnemyLaser laser;
    public Sprite sprite_shoot;
    public OctoEnemy parent;
    public EnemyDirectionState(OctoEnemy enemy){
        this.parent = enemy;
        view_area = (Area2D) enemy.GetNode("ViewArea");
        sprite = (Sprite) enemy.GetNode("Sprite");
        sprite_shoot= (Sprite) enemy.GetNode("Sprite_Shoot");
        this.laser = (EnemyLaser ) parent.GetNode("AimLaser");
    }
    public abstract void enter();
    public abstract void exit();
    public abstract void _PhysicsProcess(float delta);

}