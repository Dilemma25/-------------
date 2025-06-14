using Godot;


public partial class MessageBus : Node
{

	private static MessageBus _instance;

	public static MessageBus Instance
	{
		get
		{
			if (_instance == null)
			{
				if (Engine.GetMainLoop() is SceneTree tree)
				{
					_instance = tree.Root.GetNode<MessageBus>("MessageBus");
				}
				if (_instance == null)
				{
					GD.PrintErr("MessageBus singleton instance not found! Add MessageBus node to scene tree.");
				}
			}
			return _instance;
		}
	}

	public override void _Ready()
	{
		if (_instance != null)
		{
			QueueFree(); // Удаляем дубликат
			return;
		}
		_instance = this;
	}

	[Signal]
	public delegate void PickupMoneyEventHandler();

	public void EmitPickupMoneySignal()
	{
		EmitSignal(SignalName.PickupMoney);
	}

}
