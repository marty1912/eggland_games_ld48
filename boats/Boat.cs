using Godot;
using System;

public class Boat : RigidBody2D
{

    
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.setDirection(new DirectionStateRight(this));

    }

    /// <summary>
    /// this is the amount of steering force we can have. each direction has two  
    /// </summary>

    [Export] public float thrust_up = -10;
    [Export] public float thrust_down = 10;
    [Export] public float thrust_forward = 10;
    [Export] public float thrust_backward = -5;
    public float direction = 1;


    public DirectionState current_direction = null;

    public void setDirection(DirectionState direction) {
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
    /// this function gets all the inputs 
    /// </summary>
    /// <param name="event"></param>
    public override void _Input(InputEvent @event){
            GD.Print("got event:_", @event);
        if(@event.IsAction("FLIP_X") && !@event.IsEcho() && @event.IsPressed()){
            GD.Print("now seting dir:_", @event);
            if(this.current_direction is DirectionStateRight){
                this.setDirection(new DirectionStateLeft(this));
            }
            else{
                this.setDirection(new DirectionStateRight(this));

            }
        }
    }

    /// <summary>
    /// in this function we apply the forces we get from the user input. 
    /// </summary>
    /// <param name="state"></param>
 public override void _IntegrateForces(Physics2DDirectBodyState state)
    {

        //AppliedForce =  new Vector2(0,0);
        //AppliedTorque = 0;
        //tailFinDrag(state);
        //applyThrust(state);
        //applyRudder(state);
        //applyLift(state);


    }
    public override void _PhysicsProcess(float delta){
        AppliedForce = getCurrentThrust();
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
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
}

class DirectionStateLeft : DirectionState{
    public DirectionStateLeft(Boat parent) : base(parent){
    }
    override public void enter(){
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
            thrust.x += parent.thrust_forward; 
        }
        if (Input.IsActionPressed("GO_RIGHT"))
        { 
            thrust.x += parent.thrust_backward; 
            }


        return thrust;

    }
}


class DirectionStateRight: DirectionState{
    public DirectionStateRight(Boat parent) : base(parent){
    }
    override public void enter(){
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
}
