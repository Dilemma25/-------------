using Godot;
using System;

public partial class SpellButton : BaseButton
{
	[Export]
	NodePath cooldownBarPath;
	[Export]
	NodePath cooldownPath;

	private TextureProgressBar cooldownBarNode;
	private Label cooldownNode;

	private void GetNodesByPath()
	{
		cooldownBarNode = GetNode<TextureProgressBar>(cooldownBarPath);
		cooldownNode = GetNode<Label>(cooldownPath);
	}

	public override void _Ready()
	{
		base._Ready();

		GetNodesByPath();


		GD.Print($"бар: {cooldownBarNode.Name}, лабел бар: {cooldownNode.Name}, лабел кей: {keyNode.Name}");
	}
}
