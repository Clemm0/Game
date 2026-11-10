using System;
using Game.Core.Combat;
using Game.Core.Rpg;

namespace Game.Core.Rpg.Classes.Mage;

public class Fireball : Skill
{
    public Fireball()
    {
        Name = "Fireball";
        Description = "Hurls a ball of fire for 3d6 + INTx2 damage.";
        CooldownTurns = 3;
    }

    public override void Activate(Character attacker, Character target)
    {
        var atkStats = attacker.GetEffectiveStats();
        int roll = Dice.RollD20();
        int intMod = (int)atkStats.Primary[PrimaryStat.INT].Effective;
        int total = roll + attacker.ProficiencyBonus + intMod;
        int targetAC = (int)target.GetEffectiveStats().Defense.Effective;

        Console.WriteLine($"  *{attacker.Name} casts {Name}* (Roll: {total} vs AC {targetAC})");

        if (roll == 20 || total >= targetAC)
        {
            int dmg = Dice.Roll(3, 6) + (intMod * 2);
            target.Health -= dmg;
            Console.WriteLine($"  -> FIREBALL HITS {target.Name} for {dmg} damage! (HP: {Math.Max(0, target.Health):F0})");
        }
        else
        {
            Console.WriteLine($"  -> The spell fizzles.");
        }

        PutOnCooldown();
    }
}