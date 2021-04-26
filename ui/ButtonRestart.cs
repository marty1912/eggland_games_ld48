using Godot;
using System;

public class ButtonRestart : Button
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void _on_RetryButton_button_down(){
        _on_Button_button_down();

    }
    public void _on_Button_button_down(){
        PlayerStats.Instance.reset();
        GD.Print("button restart pressed");
        ((Transition)GetNode("/root/Transition")).fadeTo("res://levels/Level2.tscn");

    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
