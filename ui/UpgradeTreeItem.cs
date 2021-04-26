using Godot;
using System;

public class UpgradeTreeItem : Control
{
    private bool _bought = false;
    public bool Bought {
        get
        {
            return _bought;
        }
        set
        {
            if (value)
            {
                //priceLabel.Modulate = new Color(1.0f, 1.0f, 1.0f, 0.3f);
                buyButton.Disabled = true;
                outline.Modulate = new Color(1.0f, 1.0f, 0.0f, 1.0f);
            }
            else
            {
                outline.Modulate = new Color(0.2f, 0.2f, 0.2f, 1.0f);
            }
            _bought = value;
        }
    }
    
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

    [Signal]
    public delegate void buyItemSignal(UpgradeTree.UpgradeType type, int price);

    public void buyItem()
    {
        EmitSignal(nameof(buyItemSignal), upgradeType, Price);
    }

    [Export]
    public int Price = 200;

    [Export]
    public Texture upgradeIconTexture;

    [Export]
    public UpgradeTree.UpgradeType upgradeType;

    private NinePatchRect outline;
    private Button buyButton;
    private TextureRect _upgradeIcon;

    public override void _Ready()
    {
        outline = GetNode<NinePatchRect>("UpgradeOutline");
        buyButton = GetNode<Button>("BuyButton");
        _upgradeIcon = GetNode<TextureRect>("UpgradeIcon");
        buyButton.Text = Price.ToString();
        _upgradeIcon.Texture = upgradeIconTexture;
        Bought = false;
    }


    private void _on_BuyButton_pressed()
    {
        if (Price > PlayerStats.Instance.MineralCount)
        {
            return;
        }

        if (upgradeType == UpgradeTree.UpgradeType.SHIELD)
        {
            PlayerStats.Instance.ShieldCount += 1;

            if (PlayerStats.Instance.ShieldCount == PlayerStats.MAX_SHIELD_COUNT)
            {
                buyButton.Disabled = true;
            }
        }
        buyItem();
    }
}
