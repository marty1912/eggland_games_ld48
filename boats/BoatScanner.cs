using Godot;
using System;

public class BoatScanner : Scanner 
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        MaxDistance = 1000;

    }
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        if(Input.IsActionPressed("SCAN")){
            GD.Print("now scanning radius..");
            //this.ScanRadius(1000, GetWorld2d().DirectSpaceState);
            IScannable obj = (IScannable)GetParent().GetParent().GetNode("Scannable_Rock");
            this.scanObject(obj);
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
