using Godot;
using System;

public partial class Chest : Area2D, IInteractable
{
	public bool IsInteractable { get; private set; } = true;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public InteractionCommand? Interact()
	{
		if (!IsInteractable)
			return null;	
		
		IsInteractable = false;

		return new PickUpItemCommand("sword");
	}
}
