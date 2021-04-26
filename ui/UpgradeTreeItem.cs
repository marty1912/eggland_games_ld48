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

    public virtual void buyItem()
    {
        GD.Print("buy item for the low price of.",Price);
        PlayerStats.Instance.MineralCount -= Price;

        // EmitSignal(nameof(buyItemSignal), upgradeType, Price);
    }

    private int price = 0;

    [Export]
    public int Price
    {
        set
        {
            price = value;
            GD.Print("price:",price,"mineralcount:",PlayerStats.Instance.MineralCount);
            // before we init the text..
            updateButton();


        }
        get
        {
            return price;
        }
    }

/// <summary>
/// updates our button state
/// </summary>
    public virtual void updateButton(){

            GD.Print("update button baseitem");

            if(buyButton == null){
                return;
            }
            if(Price > PlayerStats.Instance.MineralCount){
                buyButton.Disabled = true;
            }
            else{
                buyButton.Disabled = false;
            }
            buyButton.Text = Price.ToString();
    }

    [Export]
    public Texture upgradeIconTexture;

    [Export]
    public UpgradeTree.UpgradeType upgradeType;

    private NinePatchRect outline;
    public Button buyButton;
    private TextureRect _upgradeIcon;

    public override void _Ready()
    {
        outline = GetNode<NinePatchRect>("UpgradeOutline");
        buyButton = GetNode<Button>("BuyButton");
        _upgradeIcon = GetNode<TextureRect>("UpgradeIcon");
        buyButton.Text = Price.ToString();
        _upgradeIcon.Texture = upgradeIconTexture;
        Bought = false;

        PlayerStats.Instance.Connect("MineralCountUpdated", this, nameof(onMineralCountUpdated));
        // this is done so we update the button visibility.
        this.Price = Price;
    }


    private void onMineralCountUpdated(int count){
        updateButton();
    }



      private void _on_BuyButton_pressed()
    {
        if (Price > PlayerStats.Instance.MineralCount)
        {
            return;
        }

        GD.Print("will now buy item..");
        buyItem();
    }
}
