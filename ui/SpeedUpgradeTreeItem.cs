using Godot;
using System;

public class SpeedUpgradeTreeItem : UpgradeTreeItem 
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
        GD.Print("bought speed upgrade");
        EmitSignal(nameof(buyItemSignal), upgradeType, Price);
        GlobalEvents.Instance.EmitSignal("SpeedUpgrade");
        Price *= 2;
    }
    public override void _Ready(){
        base._Ready();
    }



}
