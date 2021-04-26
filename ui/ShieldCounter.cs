using Godot;
using System;
using System.Collections.Generic;

public class ShieldCounter : NinePatchRect
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    public NodePath iconShield0 = "TextureRect";

    [Export]
    public NodePath iconShield1 = "TextureRect2";
    [Export]
    public NodePath iconShield2 = "TextureRect3";
    [Export]
    public NodePath iconShield3 = "TextureRect4";

    public List<TextureRect> shieldIcons;

    private Label countLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        shieldIcons = new List<TextureRect>();
        shieldIcons.Add((TextureRect)GetNode(iconShield0));
        shieldIcons.Add((TextureRect)GetNode(iconShield1));
        shieldIcons.Add((TextureRect)GetNode(iconShield2));
        shieldIcons.Add((TextureRect)GetNode(iconShield3));

        GetParent().GetNode("UpgradeTree").Connect("buyUpgradeSignal", this, nameof(OnItemBought));
        PlayerStats.Instance.Connect("ShieldCountUpdated", this, nameof(onShieldCountupdate));
        //var shieldUpgrade = GetParent().GetNode("UpgradeTree").GetNode("ShieldUpgrade"); //GetTree().CurrentScene.GetNode("Boat").GetNode("Scanner");
        //shieldUpgrade.Connect("buyItemSignal", this, nameof(OnItemBought));
        countLabel = GetNode<Label>("Count");
        onShieldCountupdate(0);
    }

    private void onShieldCountupdate(int count){
        GD.Print("got notified for shieldcount:", count);
        countLabel.Text = PlayerStats.Instance.ShieldCount.ToString();
        count = PlayerStats.Instance.ShieldCount;
        TextureRect[] icons = shieldIcons.ToArray();
        for (int i = 0; i < icons.Length;i++){
            icons[i].Visible = false;
        }
        for (int i = 0; i < icons.Length;i++){
            if (i > count)
            {
                break;
            }
            icons[i].Visible = true;
        }
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
