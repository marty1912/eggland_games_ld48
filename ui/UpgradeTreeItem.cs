using Godot;
using System;

public class UpgradeTreeItem : MarginContainer
{
    public bool Bought { get; set; }

    [Export]
    public int Price = 200;
    
    [Export]
    public float SomethingElse = 40;

    public override void _Ready()
    {
        var priceLabel = GetNode<Label>("PriceLabel");
        priceLabel.Text = Price.ToString();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
