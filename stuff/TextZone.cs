using Godot;
using System;

public class TextZone : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    public string Text = "Test TExt...";

    [Export]
    public NodePath LabelName = "Label";

    [Export]
    public NodePath TweenName = "Tween";

    public Label label;
    public Tween tween;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        label = ((Label)GetNode(LabelName));
        tween = ((Tween)GetNode(TweenName));
        label.Text = Text;
        label.Visible = false;

    }
    public void _on_TextZone_body_entered(Node body){
        if (body is Boat)
        {
        GD.Print("TextZone enter");
            label.Visible = true;
            tween.StopAll();
            tween.InterpolateProperty(label, "modulate:a", 0, 1, 0.5f);
            tween.Start();
            //GlobalEvents.Instance.EmitSignal("TextZoneEnter");
        }
    }
    public void _on_TextZone_body_exited(Node body){
        if (body is Boat)
        {
        GD.Print("TextZone exit");
            tween.StopAll();
            tween.InterpolateProperty(label, "modulate:a", 1, 0, 0.5f);
            tween.Start();
            //GlobalEvents.Instance.EmitSignal("TextZoneExit");
        }
    }

}
