using Godot;
using System;

public class Scannable_Rock : RigidBody2D, IScannable
{

    ScanState current_scanstate = null;
    public NodePath getSpriteNode(){
        return "Sprite";
    }
    public void setScanState(ScanState next_state){
        if(this.current_scanstate != null){
            current_scanstate.exit();
        }
        current_scanstate = next_state;
        current_scanstate.enter();
    }
    public Node2D GetMyNode2D(){
        return (Node2D)this;

    }
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        if (this.current_scanstate != null)
        {
            this.current_scanstate.PhysicsProcess(delta);
        }
    }

}
