using Godot;
using System;

[Tool]
public class SliceableObject2D: RigidBody2D{


    [Export] public NodePath sprite = "Sprite";
    [Export] public NodePath sprite2 = "Sprite2";
    [Export] public NodePath collisionPoly= "CollisionPoly";

}