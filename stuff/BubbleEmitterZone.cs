using Godot;
using System;

public class BubbleEmitterZone : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";


    [Export]
    public NodePath EmitterName = "Bubbles";

    [Export]
    public NodePath TweenName = "Tween";

    public CPUParticles2D particles;
    public Tween tween;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        particles= ((CPUParticles2D)GetNode(EmitterName));
        particles.Visible = false;

    }
    public void _on_BubbleEmitterZone_body_entered(Node body){
        if (body is Boat)
        {
        GD.Print("BubbleEmitterZone enter");
            particles.Visible = true;
            /*
            tween.StopAll();
            tween.InterpolateProperty(label, "modulate:a", 0, 1, 0.5f);
            tween.Start();
            */
            //GlobalEvents.Instance.EmitSignal("BubbleEmitterZoneEnter");
        }
    }
    public void _on_BubbleEmitterZone_body_exited(Node body){
        if (body is Boat)
        {
        GD.Print("BubbleEmitterZone exit");
            /*
            tween.StopAll();
            tween.InterpolateProperty(label, "modulate:a", 1, 0, 0.5f);
            tween.Start();
            */
            //GlobalEvents.Instance.EmitSignal("BubbleEmitterZoneExit");
        }
    }

}
