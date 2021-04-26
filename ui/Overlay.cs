using Godot;
using System;

public class Overlay : Control
{
    private UpgradeTree upgradeTree;

    public override void _Ready()
    {
        upgradeTree = (UpgradeTree) GetNode("UpgradeTree");
        GlobalEvents.Instance.Connect("ShopEnter", this, nameof(_on_ShopEnter));
        GlobalEvents.Instance.Connect("ShopExit", this, nameof(_on_ShopExit));
    }

    public void _on_ShopEnter(){
            upgradeTree.Visible = true;
    }

    public void _on_ShopExit(){
            upgradeTree.Visible = false;
    }
    public void _on_TextureButton_pressed()
    {
        if (!upgradeTree.Visible)
        {
            upgradeTree.Visible = true;
        }
        else
        {
            upgradeTree.Visible = false;
        }
    }
}
