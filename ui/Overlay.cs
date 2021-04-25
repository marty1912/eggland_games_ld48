using Godot;
using System;

public class Overlay : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    private Label mineralCount;

    public override void _Ready()
    {
        //var boat = GetTree().CurrentScene.GetNode("Boat");
        //var scanner = /*GetParent().GetParent().GetNode("Boat")*/ boat.GetNode<Scanner>("Scanner");
        //scanner.Connect("addedMinerals", this, nameof(OnMineralsAdded));
        //mineralCount = GetNode("HBoxContainer").GetNode<Label>("MineralCount");
    }

    private void OnMineralsAdded(float value)
    {
        //mineralCount.Text = value.ToString();
        //GD.Print("Added minerals (UI): " + value);
    }
}
