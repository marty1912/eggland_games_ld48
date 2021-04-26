using Godot;
using System;
public class GlobalEvents : Node2D
{

    [Signal]
    public delegate void ShopEnter();

    [Signal]
    public delegate void ShopExit();

    [Signal]
    public delegate void GameOver();

    [Signal]
    public delegate void LaserUpgrade();

    [Signal]
    public delegate void SpeedUpgrade();

    public static GlobalEvents Instance
    {
        get
        {
            lock(_lock)
            {
                if (instance == null)
                {
                    instance = new GlobalEvents();
                }
                return instance;
            }
        }
    }
    private static readonly object _lock = new object();
    private static GlobalEvents instance = null;

    private GlobalEvents()
    {
    }
}