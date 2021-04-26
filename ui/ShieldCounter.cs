using Godot;
using System;

public class ShieldCounter : NinePatchRect
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    private Label countLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetParent().GetNode("UpgradeTree").Connect("buyUpgradeSignal", this, nameof(OnItemBought));
        PlayerStats.Instance.Connect("ShieldCountUpdated", this, nameof(onShieldCountupdate));
        //var shieldUpgrade = GetParent().GetNode("UpgradeTree").GetNode("ShieldUpgrade"); //GetTree().CurrentScene.GetNode("Boat").GetNode("Scanner");
        //shieldUpgrade.Connect("buyItemSignal", this, nameof(OnItemBought));
        countLabel = GetNode<Label>("Count");
    }

    private void onShieldCountupdate(int count){
        GD.Print("got notified for shieldcount:", count);
        countLabel.Text = PlayerStats.Instance.ShieldCount.ToString();
    }
    private void OnItemBought(UpgradeTree.UpgradeType type, int value)
    {

        GD.Print("got notified onItemBought:");
        if (type != UpgradeTree.UpgradeType.SHIELD)
        {
            return;
        }
        countLabel.Text = PlayerStats.Instance.ShieldCount.ToString();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
