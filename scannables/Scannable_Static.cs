using Godot;
using System;

public class Scannable_Static : StaticBody2D, IScannable
{

	[Export]
	float minerals = 100;

	public override void _Ready()
	{
		base._Ready();
		this.setScanState(new ScanStateInactive(this));
	}
	public float getMinerals(){
		return minerals;
	}
	ScanState current_scanstate = null;
	public NodePath getSpriteNode(){
		return "Sprite";
	}
	
	public Node2D GetMyNode2D(){
		return (Node2D)this;

	}
	public override void _PhysicsProcess(float delta)
	{
		base._PhysicsProcess(delta);
		if (this.current_scanstate != null)
		{
			this.current_scanstate.PhysicsProcess(delta);
		}
	}
	public ScanState getScanState(){
		return this.current_scanstate;
	}

	public void setScanState(ScanState next_state){
		if(this.current_scanstate != null){
			current_scanstate.exit();
		}

		if(this.IsQueuedForDeletion()){
			return;
		}
		current_scanstate = next_state;
		current_scanstate.enter();
	}


}
