using Godot;

namespace Game.UI;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        var button = GetNode<Button>("VBoxContainer/StartButton");
        button.Pressed += OnStartPressed;
    }

    private void OnStartPressed()
    {
        GameState.Instance.StartNewGame();
    }
}