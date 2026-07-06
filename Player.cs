using Contrats;
using Entity;
using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	private Area2D? _target = null;


    public override void _Ready()
	{
		EventBus.ItemPickUp += SayHello;
	}

    public override void _UnhandledKeyInput(InputEvent @event)
	{
		GD.Print($"{@event.AsText()}");

		if (@event.IsActionPressed("interact"))
		{
			GD.Print("Interact input");
			switch (_target)
			{
				case null: break;
				case IInteractable interactable: interactable.Interact(); break;
				default: break;
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

	public void SayHello()
	{
		GD.Print("Say hello");
	}

	public void OnAreaEntered(Area2D area)
	{
		// if (area is IInteractable interactable)
		// {
		// 	interactable.Interact();
		// 	return;
		// }

		_target = area;
		// works the same way as the code snippet above 
		// (area as IInteractable)?.Interact();
		GD.Print($"Name: {area.Name} entered.");
	}

	public void OnAreaExited(Area2D area)
	{
		_target = null;

		GD.Print($"Name: {area.Name} exited.");
	}

    public override void _ExitTree()
	{
		EventBus.ItemPickUp -= SayHello;
	}
}
