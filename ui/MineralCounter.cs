using Godot;

public class MineralCounter : NinePatchRect
{
    private float MineralCount { get; set; }
    private Label mineralCountLabel;
    private static PackedScene floatingTextScene = (PackedScene) ResourceLoader.Load("res://ui/FloatingText.tscn");

    public override void _Ready()
    {
        var scanner = GetTree().CurrentScene.GetNode("Boat").GetNode("Scanner");
        scanner.Connect("addedMinerals", this, nameof(OnMineralsAdded));
        mineralCountLabel = GetNode<Label>("Count");
    }

    private void OnMineralsAdded(float amount)
    {
        MineralCount += amount;
        mineralCountLabel.Text = Godot.Mathf.CeilToInt(MineralCount).ToString();

        var text = (FloatingText) floatingTextScene.Instance();
        text.Init(new Vector2(40.0f, 60.0f), amount);
        //text.Animate();
        AddChild(text);
    }
}
