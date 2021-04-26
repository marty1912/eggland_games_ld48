using Godot;
using System;

public class SplashScreen : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    public float splashtime = 3f;
    public Boolean leaving_scene= false;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        Label label = (Label)GetNode("IntroText");
            Sprite sprite = (Sprite)GetNode("Sprite");

        Tween tween_pos = new Tween();
        AddChild(tween_pos);
        tween_pos.StopAll();
        tween_pos.InterpolateProperty(label, "modulate:a", 0f, 1f, 2f);
        tween_pos.Start();

        Tween tween_2 = new Tween();
        AddChild(tween_2);
        tween_2.StopAll();
        tween_2.InterpolateProperty(sprite, "modulate:a", 0f, 1f, 2f);
        tween_2.Start();

    }
    public override void _Process(float delta)
    {

        if(leaving_scene){
            return;
        }

        splashtime -= delta;
        if(splashtime <= 0){

            ((Transition)GetNode("/root/Transition")).fadeTo("res://Startscreen.tscn");
            leaving_scene = true;
        }
        

    }

}


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
