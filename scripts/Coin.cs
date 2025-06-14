using Godot;
using System;
using System.Collections;
public partial class Coin : Node2D
{
	[Export]
	NodePath pathToArea;

	public override void _Ready()
	{
		Area2D area = GetNode<Area2D>(pathToArea);
		area.BodyEntered += OnBodyEnteredEventHandler;
	}

	public void OnBodyEnteredEventHandler(Node player)
	{
		MessageBus.Instance.EmitPickupMoneySignal();
		QueueFree();
	}
}
