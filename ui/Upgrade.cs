using Godot;

public class AbstractUpgrade
{
    public enum UpgradeType
    {
        LASER_UPGRADE,
        POWER_SHIELD
    }

    public AbstractUpgrade(UpgradeType type, string description)
    {
        this.Type = type;
        this.Description = description;
    }

    public UpgradeType Type { get; }
    public string Description { get; }
}

public sealed class LaserUpgrade : AbstractUpgrade
{

    public LaserUpgrade(int multiplier) :
        base(UpgradeType.LASER_UPGRADE, "")
    {
    }
}
