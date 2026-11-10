using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Core.Rpg;

namespace Game.Core.Combat;

public class ConsoleCombatInterface : ICombatInterface
{
    public void Log(string message) => Console.WriteLine(message);

    public Task<string> PromptAsync(string message)
    {
        Console.Write(message);
        return Task.FromResult(Console.ReadLine() ?? "");
    }

    public Task<Character?> PickTargetAsync(List<Character> options, string prompt)
    {
        if (options.Count == 0) return Task.FromResult<Character?>(null);
        if (options.Count == 1) return Task.FromResult<Character?>(options[0]);

        Log(prompt);
        for (int i = 0; i < options.Count; i++)
            Log($"    {i + 1}. {options[i].Name} (HP: {options[i].Health:F0})");
        Log("    0. Back");

        int idx;
        while (!int.TryParse(PromptAsync("  > ").Result, out idx) || idx < 0 || idx > options.Count)
            Log("  Invalid input.");
        return Task.FromResult(idx == 0 ? null : options[idx - 1]);
    }

    public Task<Skill?> PickSkillAsync(List<Skill> options)
    {
        if (options.Count == 0) return Task.FromResult<Skill?>(null);

        Log("  Skills:");
        for (int i = 0; i < options.Count; i++)
            Log($"    {i + 1}. {options[i].Name}");
        Log("    0. Back");

        int idx;
        while (!int.TryParse(PromptAsync("  > ").Result, out idx) || idx < 0 || idx > options.Count)
            Log("  Invalid input.");
        return Task.FromResult(idx == 0 ? null : options[idx - 1]);
    }

    public Task<int> PickNumberAsync(List<string> labels, string prompt)
    {
        Log(prompt);
        for (int i = 0; i < labels.Count; i++)
            Log($"    {i + 1}. {labels[i]}");
        Log("    0. Back");

        int idx;
        while (!int.TryParse(PromptAsync("  > ").Result, out idx) || idx < 0 || idx > labels.Count)
            Log("  Invalid input.");
        return Task.FromResult(idx);
    }
}