using System;
using Game.Core.Combat;
using Game.Core.Rpg;

namespace Game.Core.Rpg.Classes.Warrior;

public class Cleave : Skill
{
    public Cleave()
    {
        Name = "Cleave";
        Description = "Sweeping attack for 2d6 damage.";
        CooldownTurns = 2;
    }

    public override void Activate(Character attacker, Character target)
    {
        var atkStats = attacker.GetEffectiveStats();
        int roll = Dice.RollD20();
        int aglMod = (int)atkStats.Primary[PrimaryStat.AGL].Effective;
        int profBonus = 0;
        if (attacker.BaseStats.Skills.TryGetValue("Swords", out int skillLvl)) profBonus = skillLvl;
        
        int total = roll + attacker.ProficiencyBonus + aglMod + profBonus;
        int targetAC = (int)target.GetEffectiveStats().Defense.Effective;
        
        Console.WriteLine($"  *{attacker.Name} uses {Name}* (Roll: {total} vs AC {targetAC})");

        if (roll == 20 || total >= targetAC)
        {
            int dmg = Dice.Roll(2, 6) + (int)atkStats.Primary[PrimaryStat.STR].Effective;
            target.Health -= dmg;
            Console.WriteLine($"  -> Hits {target.Name} for {dmg} damage! (HP: {Math.Max(0, target.Health):F0})");
        }
        else
        {
            Console.WriteLine($"  -> Misses!");
        }

        PutOnCooldown();
    }
}