// Importing necessary modules.
using Godot;
using System;
using System.Diagnostics;

// Declaring the partial class for the player character.
public partial class player : CharacterBody2D
{
	// Constants for movement and gravity.
	public const float Speed = 900.0f;
	public const float JumpVelocity = -1300.0f;	
	private const float TempGravity = 10000.0f;
	private const float NormalGravity = 1000.0f;
	private const float GravityChangeDuration = .2f;
	
	// Variables to track player state.
	private bool jumping = false;
	private bool lastFloor = false;
	private bool inCoyoteTime = false;
	private bool gravityChanged = false;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Initialize the gravity timer
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Apply gravity to the player.
		velocity.Y += (float)1.1*(gravity * (float)delta);

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && (IsOnFloor()))
		{
			velocity.Y = JumpVelocity+10000;
		}

		// Reset player position when specific input actions are triggered.
		if (Input.IsActionJustPressed("reset"))
		{
			Position = new Vector2(-3244, -18);
		}
		if (Input.IsActionJustPressed("area2"))
		{
			Position = new Vector2(-3100, -3100);
		}

		// Read player movement input.
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = 0;
		}

		// Apply the calculated velocity to the player and move accordingly.
		Velocity = velocity;
		MoveAndSlide();
	}
 
	// Method called when player enters area1.
	private void _on_area_2d_body_entered(Node2D CharacterBody2D)
	{
		Position = new Vector2(-3244, -18);
		GD.Print("area1");
	}

	// Method called when player enters area2.
	private void _on_area_2_body_entered(Node2D CharacterBody2D)
	{
		Position = new Vector2(-2300, -205);
		GD.Print("area2");
	}

	// Method called when player enters area3.
	private void _on_area_3_body_entered(Node2D CharacterBody2D)
	{
		Position = new Vector2(-1315, -5875);
		GD.Print("area3");
	}

	// Method called when player enters area4.
	private void _on_area_4_win_body_entered(Node2D CharacterBody2D)
	{
		Position = new Vector2(1650, -750);
		GD.Print("area4");
	}
}
