using Godot;
using System;

public class Temple : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }
    public void _on_Temple_body_entered(Node body){
        GD.Print("temple enter");
        if (body is Boat)
        {
            //GlobalEvents.Instance.EmitSignal("TempleEnter");
        }
    }
    public void _on_Temple_body_exited(Node body){
        GD.Print("temple exit");
        if (body is Boat)
        {
            //GlobalEvents.Instance.EmitSignal("TempleExit");
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
