using Godot;
using System;

public partial class CoyoteTime : Node
{
	[Export]
	public Timer CoyoteTimer;
	[Export]
	public player Player;

	double coyote_timer = 0.1;
	bool can_jump = false;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Player.Velocity;
		if (Player.IsOnFloor() && !can_jump){
			can_jump = true;
		} else if (can_jump && CoyoteTimer.IsStopped()){
			CoyoteTimer.Start();
		}

		if (can_jump){
			if (Input.IsActionJustPressed("jump")){
				velocity.Y = -750;
				can_jump = false;
			}
		}
		Player.Velocity = velocity;
	}

	public void CoyoteTimeout(){
		can_jump = false;
	}
}
