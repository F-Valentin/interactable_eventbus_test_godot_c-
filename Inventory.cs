using Godot;
using System;

public partial class Inventory : Control
{
	// Called when the node enters the scene tree for the first time.
	[Signal]
	public delegate void SlotSelectEventHandler(string slot_name);
	private ItemList? _itemList;
	public override void _Ready()
	{
		_itemList = GetNode<ItemList>("ItemList");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnItemSelected(int index)
	{
		// show panel 
		EmitSignal(SignalName.SlotSelect, "P");
		GD.Print(_itemList?.GetItemIcon(index));
	}
}
