using System;
using Game.Core.Combat;
using Game.Core.Rpg;

namespace Game.Core.Rpg.Classes.Warrior;

public class Taunt : Skill
{
    public Taunt()
    {
        Name = "Taunt";
        Description = "Forces all enemies to attack you for 1 turn.";
        CooldownTurns = 3;
    }

    public override void Activate(Character attacker, Character target)
    {
        Console.WriteLine($"  *{attacker.Name} TAUNTS the enemies! They'll focus fire on them next turn!*");
        attacker.IsTaunting = true;
        PutOnCooldown();
    }
}