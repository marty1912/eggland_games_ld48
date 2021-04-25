using Godot;
using System;

public class UpgradeTree : NinePatchRect
{

    //[Signal]
    //public delegate void boughtUpgrade(float amount);

    public void boughtUpgrade()
    {
        
    }

    public override void _Ready()
    {
        Visible = false;
    }
}
