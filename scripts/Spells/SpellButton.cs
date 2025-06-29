using Godot;
using System;

public partial class SpellButton : TextureButton
{
	[Export]
	NodePath cooldownBarPath;
	[Export]
	NodePath cooldownPath;
	[Export]
	NodePath keyPath;

	private TextureProgressBar cooldownBarNode;
	private Label keyNode;
	private Label cooldownNode;

	private string _changeKey;

	private void GetNodesByPath()
	{
		cooldownBarNode = GetNode<TextureProgressBar>(cooldownBarPath);
		cooldownNode = GetNode<Label>(cooldownPath);
		keyNode = GetNode<Label>(keyPath);
	}

	public override void _Ready()
	{
		GetNodesByPath();
		changeKey = "Q";
	}

	public String changeKey
	{
		set
		{
			_changeKey = value;
			keyNode.Text = value;

			Shortcut = new Shortcut();

			var inputKey = new InputEventKey();

			var rune = System.Text.Rune.GetRuneAt(value, 0);
			inputKey.Keycode = (Key)rune.Value;

			Shortcut.Events = [inputKey];
		}

		get => _changeKey;
	}



}
