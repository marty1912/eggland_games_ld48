using Godot;
using System;

public class Transition : CanvasLayer
{

    public String path = "";
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    public NodePath player ;
    public void fadeTo(String to){
        this.path = to;
        GD.Print("children:", GetChildCount());
        ((AnimationPlayer)GetNode("/root/Transition/animplayer")).Play("fade");
    }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }
    public void changeScene(){
        if(this.path ==""){
            return;
        }
        GetTree().ChangeScene(path);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
