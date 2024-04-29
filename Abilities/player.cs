using Godot;
using System;
using System.Diagnostics;

public partial class player : CharacterBody2D
{
	// Constants
	public const float Speed = 900.0f;
	public const float JumpVelocity = -1300.0f;	
	private const float TempGravity = 10000.0f;
	private const float NormalGravity = 1000.0f;
	private const float GravityChangeDuration = .2f;
	

	// Variables
	private bool jumping = false;
	private bool lastFloor = false;
	private bool inCoyoteTime = false;
	private bool gravityChanged = false;


	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		// Initialize the gravity timer
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

	
		// Add the gravity.
		velocity.Y += (float)1.1*(gravity * (float)delta);

		// Debug.Print(jumping.ToString());
		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && (IsOnFloor()))
		{
			velocity.Y = JumpVelocity+10000;
		}

		if (Input.IsActionJustPressed("reset"))
		{
			Position = new Vector2(-3244, -18);
		}
		if (Input.IsActionJustPressed("area2"))
		{
			Position = new Vector2(-3100, -3100);
		}
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = 0;
		}

		Velocity = velocity;
		MoveAndSlide();

	}
 
private void _on_area_2d_body_entered(Node2D CharacterBody2D)
{
	Position = new Vector2(-3244, -18);
	GD.Print("area1");
}
private void _on_area_2_body_entered(Node2D CharacterBody2D)
{
	 Position = new Vector2(-2300, -205);
	GD.Print("area2");
}
private void _on_area_3_body_entered(Node2D CharacterBody2D)
{
	 Position = new Vector2(-1315, -5875);
	GD.Print("area3");
}

private void _on_area_4_win_body_entered(Node2D CharacterBody2D)
{
	Position = new Vector2(1650, -750);
	GD.Print("area4");
}
}



