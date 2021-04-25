using Godot;
using System;

public class SliceOnClick :Slicer2D 
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }
    public Vector2[] clickList = new Vector2[2];
    public override void _Input(InputEvent @event)
    {
    // Mouse in viewport coordinates.
    if (@event is InputEventMouseButton eventMouseButton)
    {
        GD.Print("Mouse Click/Unclick is pressed: ", eventMouseButton.Pressed);
        if(eventMouseButton.Pressed){
                GD.Print("Mouse Click at: ", eventMouseButton.Position);
                //clickList[0] = GetGlobalTransform().XformInv(   eventMouseButton.Position);
                //clickList[0] =    eventMouseButton.Position;
                //clickList[0] =    eventMouseButton.GlobalPosition;
                clickList[0] = GetViewport().GlobalCanvasTransform.Xform(eventMouseButton.Position);
                clickList[0] = GetLocalMousePosition();

                GD.Print("Mouse Click at local: ", GetLocalMousePosition());
                GD.Print("Mouse Click at local: ", GetGlobalTransform().XformInv(clickList[0]));
            }
        else{
        GD.Print("Mouse Unclick at: ", eventMouseButton.Position);
            //clickList[1] = GetGlobalTransform().XformInv(eventMouseButton.Position);
        GD.Print("Mouse Unclick at global: ", GetGlobalMousePosition());
            clickList[1] = eventMouseButton.Position;
            clickList[1] = eventMouseButton.GlobalPosition;
                clickList[1] = GetViewport().GlobalCanvasTransform.Xform(eventMouseButton.Position);
                clickList[1] = GetLocalMousePosition();
                GD.Print("Mouse Unclick at local: ", GetGlobalTransform().XformInv(clickList[1]));
            GD.Print("Slicing now..");
                var list = sliceWorld(GetGlobalTransform().Xform(clickList[0]), GetGlobalTransform().Xform(clickList[1]));
                    GD.Print("list:", list);
                foreach(var item in list){
                    GD.Print("item:", item.ToString());
                }
            }
        
    }
    }
    public override void _Process(float delta)
    {
        base._Process(delta);
        Update();
    }
    public override void _Draw(){
        DrawLine(clickList[0], clickList[1], new Color(255, 0, 0), 1);

    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
