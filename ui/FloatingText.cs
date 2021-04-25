using Godot;
using System;

public class FloatingText : Position2D
{
    public string Value { get; set; }

    private Label valueText;
    private Tween tween;
    private int value = 0;
    private Vector2 offset = Vector2.Zero;

    private Vector2 velocity;

    public override void _Ready()
    {
        valueText = GetNode<Label>("Value");
        tween = GetNode<Tween>("Tween");

        var nextX = new Random().Next((int) offset.x - 10, (int) offset.x + 10);

        valueText.Text = "+ " + value;
        velocity = new Vector2(nextX, offset.y);

        tween.StopAll();
        tween.InterpolateProperty(this, "modulate:a", 1.0f, 0.0f, 2f);
        tween.Start();
 
    }

    public override void _Process(float delta)
    {
        Position += velocity * delta;
    }

    public void Init(Vector2 offset, int value)
    {
        this.offset = offset;
        this.value = value;
    }

    public void _on_Tween_tween_all_completed()
    {
        QueueFree();
    }
}
