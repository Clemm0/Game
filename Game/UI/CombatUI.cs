using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using Game.Core.Combat;
using Game.Core.Rpg;

namespace Game.UI;

public partial class CombatUI : Control, ICombatInterface
{
    [Export] public RichTextLabel LogLabel { get; set; } = null!;
    [Export] public VBoxContainer PartyContainer { get; set; } = null!;
    [Export] public VBoxContainer EnemyContainer { get; set; } = null!;
    [Export] public HBoxContainer ActionBar { get; set; } = null!;
    [Export] public Label PromptLabel { get; set; } = null!;

    private TaskCompletionSource<string>? _promptTcs;
    private TaskCompletionSource<Character?>? _targetTcs;
    private TaskCompletionSource<Skill?>? _skillTcs;

    public override async void _Ready()
    {
        var party = GameState.Instance.Party;
        var enemies = GameState.Instance.CurrentEnemies;

        foreach (var p in party) AddCharacterLabel(PartyContainer, p);
        foreach (var e in enemies) AddCharacterLabel(EnemyContainer, e);

        var combat = new CombatManager(party, enemies, autoBattle: false, ui: this);

        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

        try
        {
            await combat.StartCombatAsync();
        }
        catch (Exception ex)
        {
            GD.PrintErr($"Combat error: {ex}");
        }

        GameState.Instance.AdvanceRoom();
    }

    private void AddCharacterLabel(VBoxContainer parent, Character c)
    {
        var label = new Label { Name = $"Char_{c.Name}" };
        label.Text = $"{c.Name}: {c.Health:F0} HP";
        parent.AddChild(label);
    }

    public void Log(string message)
    {
        LogLabel.AppendText(message + "\n");
        RefreshLabels();
    }

    public async Task<string> PromptAsync(string message)
    {
        ClearActionBar();
        PromptLabel.Text = message;
        _promptTcs = new TaskCompletionSource<string>();

        var continueBtn = new Button { Text = "Continue" };
        continueBtn.Pressed += () => _promptTcs.TrySetResult("continue");
        ActionBar.AddChild(continueBtn);

        return await _promptTcs.Task;
    }

    public async Task<Character?> PickTargetAsync(List<Character> options, string prompt)
    {
        if (options.Count == 1) return options[0];

        ClearActionBar();
        PromptLabel.Text = prompt;
        _targetTcs = new TaskCompletionSource<Character?>();

        foreach (var c in options)
        {
            var btn = new Button { Text = $"{c.Name} ({c.Health:F0} HP)" };
            var captured = c;
            btn.Pressed += () => _targetTcs.TrySetResult(captured);
            ActionBar.AddChild(btn);
        }
        var back = new Button { Text = "Back" };
        back.Pressed += () => _targetTcs.TrySetResult(null);
        ActionBar.AddChild(back);

        return await _targetTcs.Task;
    }

    public async Task<Skill?> PickSkillAsync(List<Skill> options)
    {
        ClearActionBar();
        PromptLabel.Text = "Pick a skill:";
        _skillTcs = new TaskCompletionSource<Skill?>();

        foreach (var s in options)
        {
            var btn = new Button { Text = s.Name };
            var captured = s;
            btn.Pressed += () => _skillTcs.TrySetResult(captured);
            ActionBar.AddChild(btn);
        }
        var back = new Button { Text = "Back" };
        back.Pressed += () => _skillTcs.TrySetResult(null);
        ActionBar.AddChild(back);

        return await _skillTcs.Task;
    }

    public async Task<int> PickNumberAsync(List<string> labels, string prompt)
    {
        ClearActionBar();
        PromptLabel.Text = prompt;
        _promptTcs = new TaskCompletionSource<string>();

        for (int i = 0; i < labels.Count; i++)
        {
            int captured = i + 1;
            var btn = new Button { Text = labels[i] };
            btn.Pressed += () => _promptTcs.TrySetResult(captured.ToString());
            ActionBar.AddChild(btn);
        }
        var back = new Button { Text = "Back" };
        back.Pressed += () => _promptTcs.TrySetResult("0");
        ActionBar.AddChild(back);

        var result = await _promptTcs.Task;
        return int.TryParse(result, out int n) ? n : 0;
    }

    private void ClearActionBar()
    {
        foreach (var child in ActionBar.GetChildren())
        {
            child.QueueFree();
        }
    }

    private void RefreshLabels()
    {
        foreach (var p in GameState.Instance.Party)
        {
            var node = PartyContainer.GetNodeOrNull<Label>($"Char_{p.Name}");
            if (node != null) node.Text = $"{p.Name}: {p.Health:F0} HP";
        }
        foreach (var e in GameState.Instance.CurrentEnemies)
        {
            var node = EnemyContainer.GetNodeOrNull<Label>($"Char_{e.Name}");
            if (node != null) node.Text = $"{e.Name}: {e.Health:F0} HP";
        }
    }
}