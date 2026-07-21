using Godot;
using System;

public partial class ItemSlot : PanelContainer
{
    private int _quantity;

    public int Quantity
    {
        get { return _quantity; }
        set { _quantity = value; _quantityLabel.Text = value.ToString(); }
    }

    public ItemData? ItemData { get; private set; }
    private TextureRect _icon = null!;
    private Label _quantityLabel = null!;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _icon = GetNode<TextureRect>("MarginContainer/Icon");
        _quantityLabel = GetNode<Label>("QuantityLabel");
    }

    private void Update()
    {
        if (ItemData == null)
        {
            _icon.Texture = null;

            _quantityLabel.Visible = false;
            _quantityLabel.Text = "";

            Quantity = 0;
            return;
        }

        _icon.Texture = ItemData.Icon;

        if (_icon.Texture == null)
        {
            GD.PushError($"The {ItemData.DisplayName} item icon is null");
            return;
        }

        _quantityLabel.Visible = true;
        _quantityLabel.Text = Quantity.ToString();
    }

    public void SetItemData(ItemData? itemData, int quantity = 1)
    {
        if (itemData == null)
        {
            ItemData = null;
            Update();
            return;
        }
        
        if (quantity > itemData.MaxStackSize)
        {
            GD.PushError(
                $"the parameter quantity (value: {quantity}) must not be greater ",
                $"than the itemData max stack size (value: {itemData.MaxStackSize}) ",
                "fallback to the default value (1)."
            );
        }
        
        ItemData = itemData;
        Quantity = quantity <= itemData.MaxStackSize ? quantity : 1;
        Update();
    }

    public void OnMouseEntered()
    {
        InventoryManager.Instance.HoveredSlot = this;
    }

    public void OnMouseExited()
    {
        InventoryManager.Instance.HoveredSlot = null;
    }

    public override void _GuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mb && mb.ButtonIndex == MouseButton.Left && mb.Pressed)
        {
            GD.Print(ItemData?.DisplayName);
            var inventoryManager = InventoryManager.Instance;

            if (inventoryManager.HeldItemData == null)
                inventoryManager.PickUp(this);
        }
    }
}
