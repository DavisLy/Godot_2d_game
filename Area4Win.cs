using Godot;
using System;

public partial class Area4Win : Area2D
{
	public int direction = 0;
	private float speed = 6.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//if (Position.X <=-1000){
			//speed = 7.0f;
		//}else if (Position.X >= -600){
			//speed = -7.0f;
		//}
		//this.Translate(new Vector2(speed,0));
	}
}
