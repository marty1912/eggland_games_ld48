using Godot;
using System;

[Tool]
public abstract class SliceableObject2D: RigidBody2D{


     public NodePath mesh_nodename= "SpriteRightMesh";
     public NodePath sprite_nodename = "SpriteRight";
     public NodePath collisionPoly= "CollisionRight";
     public NodePath collisionPoly2= "CollisionLeft";

    public abstract NodePath getmeshNodename();
    public abstract NodePath getspriteNodename();
    public abstract NodePath getCollision1Nodename();
    public abstract NodePath getCollision2Nodename();

}