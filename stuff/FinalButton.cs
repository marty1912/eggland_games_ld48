using Godot;
using System;

public class FinalButton : Area2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";



	[Export]
	public NodePath SpriteNormalName= "SpriteNormal";
	[Export]
	public NodePath SpritePressedName= "SpritePressed";

	public Sprite spriteNormal;
	public Sprite spritePressed;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		spriteNormal = (Sprite)GetNode(SpriteNormalName);
		spritePressed = (Sprite)GetNode(SpritePressedName);
		spriteNormal.Visible = true;
		spritePressed.Visible = false;
	}
	public void _on_FinalButton_body_entered(Node body){
		if (body is Boat)
		{
		GD.Print("FinalButton enter");
			spritePressed.Visible = true;
			spriteNormal.Visible = false;
			//GlobalEvents.Instance.EmitSignal("FinalButtonEnter");
		}
	}
	public void _on_FinalButton_body_exited(Node body){
		if (body is Boat)
		{
		GD.Print("FinalButton exit");

			spritePressed.Visible = false;
			spriteNormal.Visible = true;
			//GlobalEvents.Instance.EmitSignal("FinalButtonExit");
		}
	}

}

