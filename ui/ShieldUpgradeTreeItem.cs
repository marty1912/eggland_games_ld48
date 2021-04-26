using Godot;
using System;

public class ShieldUpgradeTreeItem :UpgradeTreeItem 
{






    public override void buyItem()
    {

        if (PlayerStats.Instance.ShieldCount == PlayerStats.MAX_SHIELD_COUNT){
        return;
        }
        base.buyItem();
        PlayerStats.Instance.ShieldCount += 1;
    }
    public override void _Ready()
    {

        base._Ready();
        PlayerStats.Instance.Connect("ShieldCountUpdated", this, nameof(onShieldCountupdate));

    }


    private void onShieldCountupdate(int count){
            if (PlayerStats.Instance.ShieldCount == PlayerStats.MAX_SHIELD_COUNT)
            {
                buyButton.Disabled = true;
            }
            else{
                buyButton.Disabled = false;
            }
    }

}
