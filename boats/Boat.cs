using Godot;
using System;

public class Boat : SliceableObject2D 
{

	public Boolean block_rotation = false;

	public override NodePath getmeshNodename(){

		return current_direction.getmeshNodename();
	}
	public override NodePath getspriteNodename(){

		return current_direction.getspriteNodename();
	}
	public  override NodePath getCollision1Nodename(){
		return current_direction.getCollision1Nodename();

	}
	public override NodePath getCollision2Nodename(){
		return current_direction.getCollision2Nodename();

	}
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.setDirection(new DirectionStateRight(this));
		GlobalEvents.Instance.Connect("SpeedUpgrade", this, nameof(SpeedUpgrade));

	}

	/// <summary>
	/// this is the amount of steering force we can have. each direction has two  
	/// </summary>

	[Export] public float thrust_up = -10;
	[Export] public float thrust_down = 10;
	[Export] public float thrust_forward = 10;
	[Export] public float thrust_backward = -5;
	public float direction = 1;


	[Signal]
	public delegate void BoatDestroyed();
	public void getDestroyed(){
		EmitSignal(nameof(BoatDestroyed));
		GD.Print("boat got destroyed..");
		GlobalEvents.Instance.EmitSignal("GameOver");
	}
	public DirectionState current_direction = null;


	/// <summary>
	/// upgrades our laser stats
	/// </summary>
	public void SpeedUpgrade(){
		thrust_up *= 2;
		thrust_down *= 2;
		thrust_forward *= 2;
		thrust_backward *= 2;
		GD.Print("Speed upgrade installed");

	}
 
	
	
	public int Shields{
		get{
			return PlayerStats.Instance.ShieldCount;
		}
		set{
			PlayerStats.Instance.ShieldCount = value;
		}
	}


	public void setDirection(DirectionState direction) {
		if(block_rotation){
			return;
		}
		if(this.current_direction != null){
			this.current_direction.exit();
		}
		this.current_direction = direction;
		this.current_direction.enter();
	}

	/// <summary>
	/// gets the input and calculates the thrust vector with it. 
	/// </summary>
	/// <returns></returns>
	public Vector2 getCurrentThrust(){
		return this.current_direction.getInputThrust();
	}
	/// <summary>
	/// this function gets called each frame. in here we get our inputs and stuff.
	/// </summary>
	/// <param name="delta"></param>
	public override void _Process(float delta){
		

	}

	/// <summary>
	/// in this function we apply the forces we get from the user input. 
	/// </summary>
	/// <param name="state"></param>
 public override void _IntegrateForces(Physics2DDirectBodyState state)
	{

		AppliedForce = getCurrentThrust();
		AppliedTorque = this.Rotation * (Mathf.Pow(this.Rotation+1,2))*-100000;
	}
	/// <summary>
	/// updates the direction based on the velocity
	/// </summary>
	public void updateDirection(){
		if(LinearVelocity.x > 0 && current_direction is DirectionStateLeft){
			this.setDirection(new DirectionStateRight(this));
		}
		else if(LinearVelocity.x < 0 && current_direction is DirectionStateRight){
			this.setDirection(new DirectionStateLeft(this));
		}
	}
	public override void _PhysicsProcess(float delta){
		updateDirection();
	}
}
public abstract class DirectionState{
	public Boat parent;
	public DirectionState(Boat parent){
		this.parent = parent;
	}
	public abstract void enter();
	public abstract void exit();
	public abstract void process();
	public abstract Vector2 getInputThrust();

	public abstract NodePath getmeshNodename();
	public abstract NodePath getspriteNodename();
	public abstract NodePath getCollision1Nodename();
	public abstract NodePath getCollision2Nodename();
}


class DirectionStateLeft : DirectionState{
	public DirectionStateLeft(Boat parent) : base(parent){
	
	}
	override public void enter(){
		((MeshInstance2D)this.parent.GetNode("SpriteLeftMesh")).Visible = true;
		((MeshInstance2D)this.parent.GetNode("SpriteRightMesh")).Visible= false;

		//((CollisionPolygon2D)this.parent.GetNode("CollisionLeft")).Disabled = false;
		//((CollisionPolygon2D)this.parent.GetNode("CollisionRight")).Disabled = true;

		((Light2D)this.parent.GetNode("LightFrontLeft")).Enabled= true;
		((Light2D)this.parent.GetNode("LightFrontRight")).Enabled= false;
	}
	override public void exit(){

	}
override public void process(){

	}
override public Vector2 getInputThrust(){

		Vector2 thrust = new Vector2(0, 0);
		if(Input.IsActionPressed("ASCEND")){
			thrust.y += parent.thrust_up;
		}
		if(Input.IsActionPressed("DESCEND")){
			thrust.y += parent.thrust_down;
		}
		if (Input.IsActionPressed("GO_LEFT"))
		{ 
			thrust.x -= parent.thrust_forward; 
		}
		if (Input.IsActionPressed("GO_RIGHT"))
		{ 
			thrust.x -= parent.thrust_backward; 
			}


		return thrust;

	}

	public override NodePath getmeshNodename(){

		return "SpriteLeftMesh";
	}
	public override NodePath getspriteNodename(){

		return "SpriteLeft";
	}
	public  override NodePath getCollision1Nodename(){
		return "CollisionLeft";

	}
	public override NodePath getCollision2Nodename(){
		return "CollisionRight";
	}
 
}


class DirectionStateRight: DirectionState{
	public DirectionStateRight(Boat parent) : base(parent){
	}
	override public void enter(){

		//
		//((Sprite)this.parent.GetNode("SpriteRight")).Visible = true;
		((MeshInstance2D)this.parent.GetNode("SpriteRightMesh")).Visible = true;
		((MeshInstance2D)this.parent.GetNode("SpriteLeftMesh")).Visible = false;

		((CollisionPolygon2D)this.parent.GetNode("CollisionLeft")).Disabled = true;
		((CollisionPolygon2D)this.parent.GetNode("CollisionRight")).Disabled = false;

		((Light2D)this.parent.GetNode("LightFrontLeft")).Enabled= false;
		((Light2D)this.parent.GetNode("LightFrontRight")).Enabled= true;
	}
	override public void exit(){

	}
override public void process(){

	}
override public Vector2 getInputThrust(){

		Vector2 thrust = new Vector2(0, 0);
		if(Input.IsActionPressed("ASCEND")){
			thrust.y += parent.thrust_up;
		}
		if(Input.IsActionPressed("DESCEND")){
			thrust.y += parent.thrust_down;
		}
		if (Input.IsActionPressed("GO_RIGHT"))
		{ 
			thrust.x += parent.thrust_forward; 
		}
		if (Input.IsActionPressed("GO_LEFT"))
		{ thrust.x += parent.thrust_backward; }


		return thrust;

	}

 
public override NodePath getmeshNodename(){

		return "SpriteRightMesh";
	}
	public override NodePath getspriteNodename(){

		return "SpriteRight";
	}
	public  override NodePath getCollision1Nodename(){
		return "CollisionLeft";

	}
	public override NodePath getCollision2Nodename(){
		return "CollisionRight";
	}
}
