using Godot;

public class MineralCounter : NinePatchRect
{
    public enum MultiplierType
    {
        NONE,
        TIMES_2,
        TIMES_4,
        TIMES_8
    }

    private Label mineralCountLabel;
    private static PackedScene floatingTextScene = (PackedScene) ResourceLoader.Load("res://ui/FloatingText.tscn");
    private MultiplierType multiplier = MultiplierType.NONE;

    public override void _Ready()
    {
        var scanner = GetTree().CurrentScene.GetNode("Boat").GetNode("Scanner");
        scanner.Connect("addedMinerals", this, nameof(OnMineralsAdded));

        GetParent().GetNode("UpgradeTree").Connect("buyUpgradeSignal", this, nameof(OnItemBought));

        mineralCountLabel = GetNode<Label>("Count");
    }

    private void OnMineralsAdded(float amount)
    {
        int val = Godot.Mathf.CeilToInt(amount);

        switch (multiplier)
        {
            case MultiplierType.TIMES_2:
            {
                val *= 2;
                break;
            }
            case MultiplierType.TIMES_4:
            {
                val *= 4;
                break;
            }
            case MultiplierType.TIMES_8:
            {
                val *= 8;
                break;
            }
        }

        PlayerStats.Instance.MineralCount += val;
        mineralCountLabel.Text = PlayerStats.Instance.MineralCount.ToString();

        var text = (FloatingText) floatingTextScene.Instance();
        text.Init(new Vector2(40.0f, 60.0f), val, FloatingText.ValueType.POSITIVE);
        AddChild(text);
    }

    private void OnItemBought(UpgradeTree.UpgradeType type, int value)
    {
        PlayerStats.Instance.MineralCount -= value;
        mineralCountLabel.Text = PlayerStats.Instance.MineralCount.ToString();

        var text = (FloatingText) floatingTextScene.Instance();
        text.Init(new Vector2(40.0f, 60.0f), value, FloatingText.ValueType.NEGATIVE);
        AddChild(text);
    }
}
