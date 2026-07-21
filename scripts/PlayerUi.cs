using Godot;
using System;

public partial class PlayerUi : Control
{
	[Export]
	public Player? player = null;	

	// Called when the node enters the scene tree for the first time.
	private TextureRect? _slot1;
	public override void _Ready()
	{
		_slot1 = GetNode<TextureRect>("Slot1");
		// var mat = _slot1.Material as ShaderMaterial;

		// mat.SetShaderParameter()
		// mat.GetShaderParameter();
		GD.Print(player?.Name);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnInventorySlotSelected(string slotType)
	{
		GD.Print("player ui");

		// act on the player real slot
		switch (slotType)
		{
			case "WeaponSlot": _slot1?.GetChild<ReferenceRect>(0).Show(); break; 
			case "ConsumableSlot": break; 
			default: break;
		}
	}	
}
