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




    private int current_mineralcount = 0;
    private Label mineralCountLabel;
    private static PackedScene floatingTextScene = (PackedScene) ResourceLoader.Load("res://ui/FloatingText.tscn");
    private MultiplierType multiplier = MultiplierType.NONE;

    public override void _Ready()
    {
        var scanner = GetTree().CurrentScene.GetNode("Boat").GetNode("Scanner");
        //scanner.Connect("addedMinerals", this, nameof(OnMineralsAdded));

        //GetParent().GetNode("UpgradeTree").Connect("buyUpgradeSignal", this, nameof(OnItemBought));

        mineralCountLabel = GetNode<Label>("Count");
        PlayerStats.Instance.Connect("MineralCountUpdated", this, nameof(onMineralCountUpdated));
    }


    private void onMineralCountUpdated(int count){
        int new_count = count;
        GD.Print("got notified for mineral count update");
        if(current_mineralcount < new_count){
            OnMineralsAdded(new_count - current_mineralcount);
        }
        else{
            OnMineralDecrease(current_mineralcount- new_count );
        }
        current_mineralcount = count;
    }

    private void OnMineralsAdded(float amount)
    {
        int val = Godot.Mathf.CeilToInt(amount);

        mineralCountLabel.Text = PlayerStats.Instance.MineralCount.ToString();

        var text = (FloatingText) floatingTextScene.Instance();
        text.Init(new Vector2(40.0f, 60.0f), val, FloatingText.ValueType.POSITIVE);
        AddChild(text);
    }

    private void OnMineralDecrease( int value)
    {
        mineralCountLabel.Text = PlayerStats.Instance.MineralCount.ToString();
        var text = (FloatingText) floatingTextScene.Instance();
        text.Init(new Vector2(40.0f, 60.0f), value, FloatingText.ValueType.NEGATIVE);
        AddChild(text);
    }
    
}
