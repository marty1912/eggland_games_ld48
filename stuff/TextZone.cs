using Godot;
using System;
using System.Collections.Generic;

public class TextZone : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    public String[] Text = { "Test TExt..." ,""};

    [Export]
    public NodePath[] LabelNames;
    [Export]
    public NodePath LabelName1 = "Label";
    [Export]
    public NodePath LabelName2 = "Label2";
    [Export]
    public NodePath LabelName3 = "Label3";


    public List<Tween> tweens;
    public List<Label> labels;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        List<NodePath> label_list = new List<NodePath>();
        label_list.Add(LabelName1);
        label_list.Add(LabelName2);
        label_list.Add(LabelName3);
        LabelNames = label_list.ToArray();
        GD.Print("here");

        labels = new List<Label>();
        tweens = new List<Tween>();
        for (int i = 0; i < Text.Length;i++){
            if(i>= LabelNames.Length){
                break;
            }
            Label label =(Label) GetNode(LabelNames[i]);
            label.Text = Text[i];
            label.Visible = false;
            labels.Add(label);
            Tween tween = new Tween();
            AddChild(tween);
            tweens.Add(tween);
        }


    }
    public void _on_TextZone_body_entered(Node body){
        if (body is Boat)
        {
        GD.Print("TextZone enter");
            Label[] labels_arr = labels.ToArray();
            Tween[] tween_arr = tweens.ToArray();
            for (int i = 0; i < labels_arr.Length; i++)
            {
                Label label = labels_arr[i];
                Tween tween = tween_arr[i];
                label.Visible = true;
                tween.StopAll();
                tween.InterpolateProperty(label, "modulate:a", 0, 1, 0.5f);
                tween.Start();
            }
            //GlobalEvents.Instance.EmitSignal("TextZoneEnter");
        }
    }
    public void _on_TextZone_body_exited(Node body){
        if (body is Boat)
        {
        GD.Print("TextZone exit");

            Label[] labels_arr = labels.ToArray();
            Tween[] tween_arr = tweens.ToArray();
            for (int i = 0; i < labels_arr.Length; i++)
            {
                Label label = labels_arr[i];
                Tween tween = tween_arr[i];
                tween.StopAll();
                tween.InterpolateProperty(label, "modulate:a", 1, 0, 0.5f);
                tween.Start();
            }
            //GlobalEvents.Instance.EmitSignal("TextZoneExit");
        }
    }

}
