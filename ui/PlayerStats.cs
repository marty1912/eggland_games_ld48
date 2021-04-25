
public sealed class PlayerStats
{
    public static readonly int MAX_SHIELD_COUNT = 3;

    public static PlayerStats Instance
    {
        get
        {
            lock(_lock)
            {
                if (instance == null)
                {
                    instance = new PlayerStats();
                }
                return instance;
            }
        }
    }
    public int ShieldCount
    {
        get
        {
            return shieldCount;
        }
        set
        {
            lock (_lock)
            {
                if (value <= 3)
                {
                    shieldCount = value;
                }
            }
        }
    }

    public int MineralCount
    {
        get
        {
            return mineralCount;
        }
        set
        {
            lock(_lock)
            {
                mineralCount = value;
            }
        }
    }

    private static readonly object _lock = new object();
    private static PlayerStats instance = null;

    private PlayerStats()
    {
    }

    private int shieldCount = 0, mineralCount = 0;
}