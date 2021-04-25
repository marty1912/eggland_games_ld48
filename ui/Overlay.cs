using Godot;
using System;

public class Overlay : Control
{
    private UpgradeTree upgradeTree;

    public override void _Ready()
    {
        upgradeTree = (UpgradeTree) GetNode("UpgradeTree");
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
