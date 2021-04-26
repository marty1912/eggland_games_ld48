using Godot;
using System;

public class Temple : Area2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	[Export]
	public NodePath planSprite = "Sprite2";

	public Sprite plan;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		plan = (Sprite)GetNode(planSprite);

		plan.Visible = false;

	}
	public void _on_Temple_body_entered(Node body){
		GD.Print("temple enter");
		if (body is Boat)
		{
			plan.Visible = true;
			//GlobalEvents.Instance.EmitSignal("TempleEnter");
		}
	}
	public void _on_Temple_body_exited(Node body){
		GD.Print("temple exit");
		if (body is Boat)
		{
			plan.Visible = false;
			//GlobalEvents.Instance.EmitSignal("TempleExit");
		}
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
