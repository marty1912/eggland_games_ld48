using Godot;
using System;

public class FloatingText : Position2D
{
    public enum ValueType
    {
        POSITIVE,
        NEGATIVE
    }

    public string Value { get; set; }

    private Label valueText;
    private Tween tween;
    private int value = 0;
    private Vector2 offset = Vector2.Zero;

    private Vector2 velocity;
    private ValueType valueType;

    public override void _Ready()
    {
        valueText = GetNode<Label>("Value");
        tween = GetNode<Tween>("Tween");

        var nextX = new Random().Next((int) offset.x - 10, (int) offset.x + 10);
        string prefix;

        if (valueType == ValueType.POSITIVE)
        {
            prefix = "+ ";
            valueText.Modulate = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        }
        else
        {
            prefix = "- ";
            valueText.Modulate = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }

        valueText.Text = prefix + value;
        velocity = new Vector2(nextX, offset.y);

        tween.StopAll();
        tween.InterpolateProperty(this, "modulate:a", 1.0f, 0.0f, 2f);
        tween.Start();
 
    }

    public override void _Process(float delta)
    {
        Position += velocity * delta;
    }

    public void Init(Vector2 offset, int value, ValueType type)
    {
        this.offset = offset;
        this.value = value;
        this.valueType = type;
    }

    public void _on_Tween_tween_all_completed()
    {
        QueueFree();
    }
}
