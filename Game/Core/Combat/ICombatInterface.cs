using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Core.Rpg;

namespace Game.Core.Combat;

public interface ICombatInterface
{
    void Log(string message);
    Task<string> PromptAsync(string message);
    Task<Character?> PickTargetAsync(List<Character> options, string prompt);
    Task<Skill?> PickSkillAsync(List<Skill> options);
    Task<int> PickNumberAsync(List<string> labels, string prompt);
}