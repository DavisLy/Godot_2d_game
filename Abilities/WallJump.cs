using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class WallJump : Node
{
	[Export]
	public player Player;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	bool canDoubleJump = false;
	public const float JumpVelocity = -750.0f;	
	float wallJumpPushBack = 100.0f;
	float wallSlide = 500.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
{
	Vector2 velocity = Player.Velocity;
	velocity.Y += gravity * (float)delta;

	if (Input.IsActionJustPressed("jump"))
	{
		if (!Player.IsOnFloor())
		{
			if (Player.IsOnWallOnly() && Input.IsActionPressed("right"))
			{
				velocity.Y = JumpVelocity;
				velocity.X = -wallJumpPushBack;
			}
			else if (Player.IsOnWallOnly() && Input.IsActionPressed("left"))
			{
				velocity.Y = JumpVelocity;
				velocity.X = wallJumpPushBack;
			}
		}
	}

	Player.Velocity = velocity; // Update Player's velocity
}

}
