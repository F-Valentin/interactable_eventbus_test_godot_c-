using Godot;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	private List<Area2D?> _targets = [];

    public override void _Ready()
	{
	}

    public override void _UnhandledKeyInput(InputEvent @event)
	{
		if (@event.IsActionPressed("interact"))
		{
			foreach (var target in _targets)
			{
				if (target is IInteractable interactable)
				{
					interactable?.Interact()?.Apply(this);	
				}
			}
		}

	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		// if (!IsOnFloor())
		// {
		// 	velocity += GetGravity() * (float)delta;
		// }

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public void OnAreaEntered(Area2D area)
	{
		// if (area is IInteractable interactable)
		// {
		// 	interactable.Interact();
		// 	return;
		// }

		_targets.Add(area);
		// works the same way as the code snippet above 
		// (area as IInteractable)?.Interact();
		GD.Print($"Name: {area.Name} entered.");
	}

	public void OnAreaExited(Area2D area)
	{
		_targets.Remove(area);
		GD.Print($"Name: {area.Name} exited.");
	}

    public override void _ExitTree()
	{
	}
}
