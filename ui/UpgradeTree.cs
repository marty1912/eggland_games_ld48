using Godot;

public class UpgradeTree : NinePatchRect
{
    public enum UpgradeType
    {
        SHIELD,
        MULTIPLIER_X2,
        MULTIPLIER_X4,
        MULTIPLIER_X8,
        BOOST
    }

    [Signal]
    public delegate void buyUpgradeSignal(UpgradeType type, int value);

    public override void _Ready()
    {
        Visible = false;
        GetNode("ShieldUpgrade").Connect("buyItemSignal", this, nameof(OnItemBought));
        GetNode("LaserUpgrades").GetNode("LaserUpgrade_X2").Connect("buyItemSignal", this, nameof(OnItemBought));
        GetNode("LaserUpgrades").GetNode("LaserUpgrade_X4").Connect("buyItemSignal", this, nameof(OnItemBought));
    }

    private void OnItemBought(UpgradeType type, int value)
    {
        EmitSignal(nameof(buyUpgradeSignal), type, value);
    }
}
