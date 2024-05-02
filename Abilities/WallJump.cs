using Godot; // Importing Godot module for game development.
using System; // Importing System module for basic functionality.
using System.Security.Cryptography.X509Certificates; // Importing X509Certificates for certificate management.
using System.Threading; // Importing Threading for managing threads.

// Declaring the partial class for wall jumping functionality.
public partial class WallJump : Node
{
	[Export]
	public player Player; // Reference to the player object.

	// Gravity value retrieved from project settings.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	// Variables related to wall jumping.
	bool canDoubleJump = false;
	bool isWallSliding = false;
	public const float wallJumpVelocity = -1000.0f;
	float wallJumpPushBack = -1000.0f;
	float wallSlidingGravity = 500.0f;
	public const float JumpVelocity = -1300.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Initialization code can be added here if needed.
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Player.Velocity;

		// Handle jumping input.
		if (Input.IsActionJustPressed("jump") && (Player.IsOnFloor()))
		{
			velocity.Y = JumpVelocity;
		}

		// Handle wall jumping input.
		if (Input.IsActionJustPressed("jump"))
		{
			if (Player.IsOnWallOnly() && Input.IsActionPressed("right"))
			{
				velocity.Y = wallJumpVelocity;
				//velocity.X = -wallJumpPushBack;
				GD.Print("wall jump1");
			}
			else if (Player.IsOnWallOnly() && Input.IsActionPressed("left"))
			{
				velocity.Y = wallJumpVelocity;
				//velocity.X = wallJumpPushBack;
				GD.Print("wall jump");
			}
			wallSlide(delta); // Check for wall sliding.
		}

		Player.Velocity = velocity; // Update Player's velocity.
	}

	// Method to handle wall sliding.
	public void wallSlide(double delta)
	{
		Vector2 velocity = Player.Velocity;

		// Check if player is on a wall and not on the floor.
		if (Player.IsOnWall() && !Player.IsOnFloor())
		{
			// Determine if the player is pressing left or right.
			if (Input.IsActionPressed("left") || Input.IsActionPressed("right"))
			{
				isWallSliding = true;
			}
			else
			{
				isWallSliding = false;
			}
		}
		else
		{
			isWallSliding = false;
		}

		// Apply wall sliding behavior if applicable.
		if (isWallSliding)
		{
			velocity.Y += (float)(wallSlidingGravity * delta);
			velocity.Y = Math.Min(velocity.Y, gravity - 100);
			GD.Print("hi");
		}

		Player.Velocity = velocity; // Update Player's velocity.
	}
}
