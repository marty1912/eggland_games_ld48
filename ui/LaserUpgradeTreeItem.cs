using Godot;
using System;

public class LaserUpgradeTreeItem : UpgradeTreeItem 
{
   /*
    public enum ItemType
    {
        SHIELD,
        MULTIPLIER_X2,
        MULTIPLIER_X4,
        MULTIPLIER_X5,
        BOOST
    }
    */


    public override void buyItem()
    {

        base.buyItem();
        GD.Print("bought laser upgrade");
        EmitSignal(nameof(buyItemSignal), upgradeType, Price);
        GlobalEvents.Instance.EmitSignal("LaserUpgrade");
        Price *= 2;
    }
    public override void _Ready(){
        base._Ready();
    }



}
