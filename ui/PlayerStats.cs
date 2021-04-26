using Godot;
using System;
public class PlayerStats : Node2D
{

    [Signal]
    public delegate void ShieldCountUpdated(int count);

    [Signal]
    public delegate void MineralCountUpdated(int count);
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

    public void reset(){
        instance = new PlayerStats();
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
                if (value <= MAX_SHIELD_COUNT)
                {
                    shieldCount = value;
                    GD.Print("now emitting signal:",value);
                    Instance.EmitSignal(nameof(ShieldCountUpdated), Instance.shieldCount);
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

                    Instance.EmitSignal(nameof(MineralCountUpdated), Instance.mineralCount);
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