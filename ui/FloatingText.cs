using Godot;
using System;

public class FloatingText : Position2D
{
    public string Value { get; set; }

    private Label valueText;
    private Tween tween;

    private Vector2 velocity;

    public override void _Ready()
    {
    }

    public override void _Process(float delta)
    {
        Position += velocity * delta;
    }

    public void Init(Vector2 offset, float value)
    {
        valueText = GetNode<Label>("Value");
        tween = GetNode<Tween>("Tween");

        var nextX = new Random().Next((int) offset.x - 10, (int) offset.x + 10);
        //var nextY = new Random().Next((int) offset.y - 10, (int) offset.y + 10);

        valueText.Text = "+ " + value;
        velocity = new Vector2(nextX, offset.y);

        tween.StopAll();
        //tween.InterpolateProperty(this, "Scale", Scale, new Vector2(0.2f, 0.2f), 1.0f, Tween.TransitionType.Linear, Tween.EaseType.Out);
        tween.InterpolateProperty(this, "modulate:a", 1.0f, 0.0f, 2f);
        //AddChild(tween);
        tween.Start();
    }

    public void Animate()
    {
        
    }

    public void _on_Tween_tween_all_completed()
    {
        GD.Print("Completed tween ------------->");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
