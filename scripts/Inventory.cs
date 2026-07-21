using Godot;
using System;

public partial class Inventory : Control
{
	// Called when the node enters the scene tree for the first time.
	[Signal]
	public delegate void SlotSelectEventHandler(string slot_name);


	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnItemSelected(int index)
    {
	    /*
			if (item.Type == "Weapon")
			    EmitSignal(SignalName.SlotSelect, "WeaponSlot")
			else if (item.Type == "Consumable")
			    EmitSignal(SignalName.SlotSelect, "ComsumableSlot")
		*/
		EmitSignal(SignalName.SlotSelect, "WeaponSlot");
	}
}
