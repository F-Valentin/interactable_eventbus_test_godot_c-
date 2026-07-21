using Godot;
using System;

public partial class InventoryManager : Node
{
    public static InventoryManager Instance { get; private set; } = null!;

    public ItemData HeldItemData { get; set; } = null!;
    public int HeldQuantity { get; set; }
    public ItemSlot? SourceSlot { get; set; } = null;
    public ItemSlot? HoveredSlot { get; set; } = null;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Instance = this;
    }

    public void PickUp(ItemSlot slot)
    {
        if (slot == null)
        {
            GD.PrintErr("slot is null");
            return;
        }
        if (slot.ItemData == null)
            return;

        HeldItemData = slot.ItemData;
        HeldQuantity = slot.Quantity;
        SourceSlot = slot;
        slot.SetItemData(null, 0);
    }

    public void CancelPickUp()
    {
        try
        {
            SourceSlot?.SetItemData(HeldItemData, HeldQuantity);
        }
        catch (NullReferenceException e)
        {
            GD.PushError(e);
            return;
        }

        Reset();
    }

    private void Reset()
    {
        HeldItemData = null!;
        HeldQuantity = 0;
        SourceSlot = null;
    }

    private int Stack(ItemSlot target)
    {
        if (target.ItemData == null)
            return -1;

        int space = target.ItemData.MaxStackSize - target.Quantity;

        if (HeldQuantity == HeldItemData.MaxStackSize || space <= 0)
            return 0;

        var transfer = int.Min(HeldQuantity, space);

        target.Quantity += transfer;
        HeldQuantity -= transfer;

        if (HeldQuantity <= 0)
            Reset();
        else if (SourceSlot != null)
            SourceSlot.SetItemData(HeldItemData, HeldQuantity);
        else
            GD.PushError("Source Slot must not be null here");

        return 1;
    }

    private void Swap(ItemSlot target)
    {
        var tmp_item = target.ItemData;
        var tmp_qty = target.Quantity;

        if (tmp_item == null)
            return;

        target.SetItemData(HeldItemData, HeldQuantity);
        SourceSlot?.SetItemData(tmp_item, tmp_qty);
        Reset();
    }
    
    private void TryStackOrSwap(ItemSlot target)
    {
        if (target.ItemData?.id == HeldItemData.id)
        {
            if (Stack(target) == 0)
                Swap(target);
        }
        else
            Swap(target);
    }

    private void DropOn(ItemSlot target)
    {
        if (HeldItemData == null)
            return;

        if (target.ItemData == null)
            target?.SetItemData(HeldItemData, HeldQuantity);
        else
            TryStackOrSwap(target);
        
        Reset();
    }

	public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mb && mb.ButtonIndex == MouseButton.Left && !mb.Pressed)
        {
            GD.Print($"HoveredSlot: {HoveredSlot?.Name}");
            
            if (HoveredSlot != null)
                DropOn(HoveredSlot);
        }
	}
}
