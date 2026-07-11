using Godot;
using System;

public partial class PlayerUi : Control
{
	[Export]
	public Player? player = null;	

	// Called when the node enters the scene tree for the first time.
	private Label? _slot1;
	public override void _Ready()
	{
		// _slot1 = GetNode<Label>("Slot1");
		// var mat = _slot1.Material as ShaderMaterial;

		// mat.SetShaderParameter()
		// mat.GetShaderParameter();
		GD.Print(player?.Name);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnInventorySlotSelected(string slot_name)
	{
		GD.Print("player ui");
		GD.Print(slot_name);

		// act on the player real slot
		switch (slot_name)
		{
			case "up": break; 
			default: break;
		}
	}	
}
