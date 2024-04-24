using Godot;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

public partial class WallJump : Node
{
	
	[Export]
	public player Player;
	
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	bool canDoubleJump = false;
	bool isWallSliding = false;
	public const float wallJumpVelocity = -1200.0f;
	float wallJumpPushBack = -1000.0f;
	float wallSlidingGravity = 500.0f;
	public const float JumpVelocity = -1300.0f;	


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Player.Velocity;
		if (Input.IsActionJustPressed("jump") && (Player.IsOnFloor()))
		{
			velocity.Y = JumpVelocity;
		}
		if (Input.IsActionJustPressed("jump"))
		{
			if (Player.IsOnWallOnly() && Input.IsActionPressed("right"))
			{
				velocity.Y = wallJumpVelocity;
				velocity.X = -wallJumpPushBack;
				GD.Print("wall jump1");
			}
			else if (Player.IsOnWallOnly() && Input.IsActionPressed("left"))
			{
				velocity.Y = wallJumpVelocity;
				velocity.X = wallJumpPushBack;
				GD.Print("wall jump");
			}
			wallSlide(delta);
		}

		Player.Velocity = velocity; // Update Player's velocity

	}
	public void wallSlide(double delta){
		Vector2 velocity = Player.Velocity;
		if (Player.IsOnWall() && !Player.IsOnFloor()){
			if(Input.IsActionPressed("left") ||Input.IsActionPressed("right")){
				isWallSliding = true;
			}else{
				isWallSliding = false;
			}
		}else{
			isWallSliding =false;
		}
		if (isWallSliding)
		{
			velocity.Y += (float)(wallSlidingGravity * delta);
			velocity.Y = Math.Min(velocity.Y, gravity-100);
		GD.Print("hi");	
		}

		Player.Velocity = velocity;

	}
}
