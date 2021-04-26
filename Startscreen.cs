using Godot;
using System;

public class Startscreen : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    public String[] WriteText = {
        "It is the year 2077.",
        "Humanity started a mission to explore Jupiters Moon Europa.",
        "Europa is covered in ice, but underneath the surface, lies a vast ocean.",
        "Humans suspected vast amounts of kroterium, a new power source beneath the surface.",
        "The mission carried a submarine, controlled by the advanced artificial intelligence system.",
        "This system is called the \"Yokohama Outerspace U-boat control\" (Y.O.U.).",
        "This is its story.",
        "" ,"","" ,""  };

    [Export]
    public float time_per_text =5f;


    [Export]
    public float text_tween_time=25f;

    [Export]
    public NodePath baseLabel_Name;
    public Label base_label;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base_label = (Label)GetNode(baseLabel_Name);

        // start immediately
        current_time = time_per_text;
    }
    public float current_time = 0f;
    public int text_index= 0;
    public Boolean leaving_scene= false;

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

        if(leaving_scene){
            return;
        }

        if(text_index >= WriteText.Length){
            // finished..
            ((Transition)GetNode("/root/Transition")).fadeTo("res://levels/Level2.tscn");
            leaving_scene = true;
            //GetTree().ChangeScene("res://levels/Level2.tscn");
            return;
        }

        current_time += delta;
        if(current_time >= time_per_text){
            Label cur_label = (Label)base_label.Duplicate();
            AddChild(cur_label);
            
            /// move upwards
            Tween tween_pos = new Tween();
            AddChild(tween_pos);
            tween_pos.StopAll();
            tween_pos.InterpolateProperty(cur_label, "rect_position:y", 300, -200, text_tween_time,Tween.TransitionType.Quart,Tween.EaseType.Out);
            tween_pos.Start();

            Tween tween_size = new Tween();
            AddChild(tween_size);
            tween_size.StopAll();
            tween_size.InterpolateProperty(cur_label, "rect_scale", new Vector2(1,1), new Vector2(0,0),text_tween_time,Tween.TransitionType.Quart,Tween.EaseType.Out);
            tween_size.Start();

            cur_label.Text = WriteText[text_index];
            cur_label.RectScale = Vector2.Zero;

            current_time = 0f;
            text_index++;
        }
        

    }

}
