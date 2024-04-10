using Godot;
using System;
using System.Diagnostics;

public partial class player : CharacterBody2D
{
	// Constants
	public const float Speed = 250.0f;
	public const float JumpVelocity = -750.0f;	
	private const float TempGravity = 10000.0f;
	private const float NormalGravity = 1000.0f;
	private const float GravityChangeDuration = .2f;
	

	// Variables
	private bool jumping = false;
	private bool lastFloor = false;
	private bool inCoyoteTime = false;
	private bool gravityChanged = false;

	[Export]
	private Timer GRAVITY_TIMER;

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
		velocity.Y += gravity * (float)delta;

		Debug.Print(jumping.ToString());
		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && (IsOnFloor() || inCoyoteTime && !jumping))
		{
			velocity.Y = JumpVelocity;
			jumping = true;
		}

		if (Input.IsActionJustPressed("reset"))
		{
			Position = new Vector2(36, -650);
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
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

		if (!IsOnFloor() && !jumping)
		{
			inCoyoteTime = true;
			GRAVITY_TIMER.Start();
		}

		if (IsOnFloor()){
			jumping = false;
		}
	}
 
	private void OnCoyoteTimerTimeout()
	{
		inCoyoteTime = false;
	}
}

