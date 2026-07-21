using Godot;
using System;

public partial class InventoryGrid : PanelContainer
{
    [Export]
    private int _slotCount = 0;

    [Export]
    PackedScene? SlotScene;

    private GridContainer? _slotGrid;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (SlotScene == null)
        {
            GD.PushError("Slot Scene is null");
            return;
        }

        _slotGrid = GetNode<GridContainer>("MarginContainer/VBoxContainer/ScrollContainer/SlotGrid");

        if (_slotGrid == null)
        {
            GD.PushError("Slot Grid is null");
            return;
        }

        BuildSlot();
    }

    private void BuildSlot()
    {
        for (int i = 0; i < _slotCount; i++)
        {
            var slot = SlotScene?.Instantiate<ItemSlot>();

            _slotGrid?.AddChild(slot);
        }

        var slot_zero = _slotGrid?.GetChild<ItemSlot>(0);
        var slot_one = _slotGrid?.GetChild<ItemSlot>(1);
        var slot_two = _slotGrid?.GetChild<ItemSlot>(2);
        var itemDataBase = ItemDataBase.Instance;

        ItemData? bomb = itemDataBase.GetItemData("bomb");
        ItemData? key = itemDataBase.GetItemData("key");

        if (bomb == null || key == null)
            return;
            
        slot_zero?.SetItemData(bomb, 7);
        slot_one?.SetItemData(bomb, 5);
        slot_two?.SetItemData(key, 1);
        
    }

    public void OnMouseExited()
    {
        if (GetRect().HasPoint(GetLocalMousePosition()))
            return;
            
        GD.Print($"MouseExited: {Name}");
        
        if (InventoryManager.Instance.HeldItemData != null)
            InventoryManager.Instance.CancelPickUp();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mb && mb.ButtonIndex == MouseButton.Right && mb.Pressed)
        {
            var inventoryManager = InventoryManager.Instance;

            if (inventoryManager.HeldItemData != null)
                inventoryManager.CancelPickUp();
        }
    }
}
