using Godot;
using System;
using Contrats;

namespace Entity;
public partial class Enemy : Area2D, IInteractable
{
	[Export]
	public bool IsInteractable { get; private set; } = true;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Interact()
	{
		if (!IsInteractable)
			return;

		GD.Print("interact");
		IsInteractable = false;

		Tween tween = CreateTween().SetParallel(true);

		tween.SetEase(Tween.EaseType.In);
		tween.TweenProperty(this, "rotation_degrees", 360.0, 1.5);

		EventBus.EmitItemPickUp();
	}	
}
