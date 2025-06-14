using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class MoneyManager : Node
{
	private int coinsCount = 0;
	public override void _Ready()
	{
		MessageBus.Instance.PickupMoney += PickupMoneyEventHandler;
	}

	private void PickupMoneyEventHandler()
	{
		coinsCount += 1;
		GD.Print("Pickup Money, now have: " + coinsCount);
	}

}
