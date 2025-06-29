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

    private ProgressBar cooldownBarNode;
    private Label keyNode;
    private Label cooldownNode;

    private void GetNodesByPath()
    {
        cooldownBarNode = GetNode<ProgressBar>(cooldownBarPath);
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
            changeKey = value;
            keyNode.Text = value;

            Shortcut = new Shortcut();

            var inputKey = new InputEventKey();

            var rune = System.Text.Rune.GetRuneAt(value, 0);
            inputKey.Keycode = (Key)rune.Value;

            Shortcut.Events = [inputKey];
        }
    }



}
