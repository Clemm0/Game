using System;
using Game.Core.Combat;
using Game.Core.Rpg;

namespace Game.Core.Rpg.Classes.Cleric;

public class Heal : Skill
{
    public Heal()
    {
        Name = "Heal";
        Description = "Restores 2d8 + (MaxHP/20) to an ally below 60% HP.";
        CooldownTurns = 3;
    }

    public override void Activate(Character attacker, Character target)
    {
        target ??= attacker;
        var atkStats = attacker.GetEffectiveStats();
        var tgtStats = target.GetEffectiveStats();

        if (target.Health / tgtStats.Health.Effective > 0.60f)
        {
            Console.WriteLine($"  *{attacker.Name} holds their heal — {target.Name} is above 60% HP.*");
            return;
        }

        int rollHeal = Dice.Roll(2, 8);
        int scalingHeal = (int)(atkStats.Health.Effective / 20f);
        int totalHeal = rollHeal + scalingHeal;

        target.Health = Math.Min(target.Health + totalHeal, tgtStats.Health.Effective);
        Console.WriteLine($"  *{attacker.Name} casts {Name} on {target.Name}* (2d8={rollHeal} + {scalingHeal} scaling = {totalHeal} HP restored. HP: {target.Health:F0}/{tgtStats.Health.Effective:F0})");

        PutOnCooldown();
    }
}