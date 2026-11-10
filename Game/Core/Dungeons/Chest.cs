using System;
using System.Collections.Generic;
using Game.Core.Combat;
using Game.Core.Items;
using Game.Core.Rpg;

namespace Game.Core.Dungeons;

public class Chest
{
    public string Name { get; set; } = "Chest";
    public bool IsLocked { get; set; }
    public bool IsOpened { get; private set; }
    public List<Item> Contents { get; set; } = new();
    public int LockDifficulty { get; set; } = 12;
    
    public void Open(Character player)
    {
        if (IsOpened)
        {
            Console.WriteLine("  The chest is already empty.");
            return;
        }

        if (IsLocked)
        {
            int pickRoll = Dice.RollD20();
            int aglMod = (int)player.GetEffectiveStats().Primary[PrimaryStat.AGL].Effective;
            int lockpickBonus = 0;
            if (player.BaseStats.Skills.TryGetValue("Lockpicking", out int level))
                lockpickBonus = level;

            int total = pickRoll + aglMod + lockpickBonus;
            Console.WriteLine($"  *Attempts to pick the lock* (Roll: {pickRoll} + {aglMod}(AGL) + {lockpickBonus}(Skill) = {total} vs DC {LockDifficulty})");

            if (total < LockDifficulty)
            {
                Console.WriteLine("  The lock holds. The chest refuses to open.");
                return;
            }
            Console.WriteLine("  *Click!* The lock gives way.");
            IsLocked = false;
        }

        IsOpened = true;
        Console.WriteLine($"  You open the {Name}. It contains:");
        foreach (var item in Contents)
        {
            Console.WriteLine($"    - {item.Name} (Weight: {item.Weight:F1}kg)");
            if (!player.Inventory.TryAddItem(item))
                Console.WriteLine($"    - {item.Name} could not be added to inventory!");
        }
    }
}