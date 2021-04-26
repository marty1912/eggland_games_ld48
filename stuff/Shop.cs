using Godot;
using System;

public class Shop : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }
    public void _on_Shop_body_entered(Node body){
        GD.Print("shop enter");
        if (body is Boat)
        {
            GlobalEvents.Instance.EmitSignal("ShopEnter");
        }
    }
    public void _on_Shop_body_exited(Node body){
        GD.Print("shop enter");
        if (body is Boat)
        {
            GlobalEvents.Instance.EmitSignal("ShopExit");
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
