using Godot;
using System;
using System.Collections.Generic;

using System.Linq;
public class BoatScanner : Scanner 
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

/// <summary>
/// upgrades our laser stats
/// </summary>
    public void LaserUpgrade(){
        MaxDistance *= 2;
        mineralMultiplier *= 2;
        maxObjects *= 2;

    }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        MaxDistance = 100;
        GlobalEvents.Instance.Connect("LaserUpgrade", this, nameof(LaserUpgrade));
        maxObjects = 1;
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        Godot.Collections.Array exceptions = new Godot.Collections.Array();
        exceptions.Add(this.GetParent());
        List<IScannable> available = this.ScanRadius(this.MaxDistance, GetWorld2d().DirectSpaceState,exceptions);
        foreach (var avail in available)
        {
            if (avail.getScanState() is ScanStateInactive)
            {
                avail.setScanState(new ScanStateHighlight(avail, this));
            }
        }

        if(Input.IsActionPressed("SCAN")){
            this.scanObjects(available.Take(this.maxObjects-currently_scanning).ToList());
            //IScannable obj = (IScannable)GetParent().GetParent().GetNode("Scannable_Rock");
            //this.scanObject(obj);
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
