using Godot;
using System;

public partial class BaseButton : TextureButton
{
    [Export]
    protected NodePath keyPath;
    protected Label keyNode;

    protected string _changeKey;

    private void GetNodesByPath()
    {
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
