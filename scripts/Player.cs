using Godot;
using System;
using System.Data.Common;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Vector2 direction = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");

		if (direction != Vector2.Zero)
		{

			if (Input.IsActionJustPressed("MoveLeft") || Input.IsActionJustPressed("MoveRight"))
			{
				velocity.X = direction.X * Speed;
			}
			else if (Input.IsActionJustPressed("MoveUp") || Input.IsActionJustPressed("MoveDown"))
			{
				velocity.Y = direction.Y * Speed;
			}
		}
		else
		{
			velocity.X = 0;
			velocity.Y = 0;
		}


		Velocity = velocity;
		MoveAndSlide();
	}
}
