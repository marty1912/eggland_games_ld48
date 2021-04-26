using Godot;
using System;

public class ShieldUpgradeTreeItem :UpgradeTreeItem 
{






    /// <summary>
    /// updates our button state
    /// </summary>
    public override void updateButton(){

        GD.Print("update button Shield item");
        if(buyButton == null){
                return;
            }
            if(Price > PlayerStats.Instance.MineralCount){
                buyButton.Disabled = true;
            }
            else{
                buyButton.Disabled = false;
            }

            if (PlayerStats.Instance.ShieldCount >= PlayerStats.MAX_SHIELD_COUNT)
            {
                buyButton.Disabled = true;
            }

        buyButton.Text = Price.ToString();
    }



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

        Price = prices[PlayerStats.Instance.ShieldCount - 1];
    }

}
