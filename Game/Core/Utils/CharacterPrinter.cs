using System;
using System.Linq;
using Game.Core.Rpg;
using Game.Core.Rpg.Equipment;

namespace Game.Core.Utils;

public static class CharacterPrinter
{
    public static void PrintCharacterSheet(Character c)
    {
        var stats = c.GetEffectiveStats();
        Console.WriteLine($"\n╔══════════════════════════════════════════╗");
        Console.WriteLine($"║ {c.Name} (Level {c.Level})                      ");
        Console.WriteLine($"╠══════════════════════════════════════════╣");
        Console.WriteLine($"║ HP: {c.Health:F0}/{stats.Health.Effective:F0}   MP: {c.Mana:F0}   Stamina: {c.Stamina:F0}");
        Console.WriteLine($"║ STR: {stats.Primary[PrimaryStat.STR].Effective}   AGL: {stats.Primary[PrimaryStat.AGL].Effective}   INT: {stats.Primary[PrimaryStat.INT].Effective}");
        Console.WriteLine($"║ DEF: {stats.Defense.Effective:F0}   ATK: {stats.AttackPower.Effective:F0}   SPELL: {stats.SpellPower.Effective:F0}");
        Console.WriteLine($"╠══════════════════════════════════════════╣");
        Console.WriteLine("║ EQUIPPED:");
        
        if (c.Equipment.EquippedItems.Count == 0)
            Console.WriteLine("║   (Nothing equipped)");
        else
            foreach (var kvp in c.Equipment.EquippedItems)
                Console.WriteLine($"║   [{kvp.Key,-8}] {kvp.Value.Name}");

        Console.WriteLine("╠══════════════════════════════════════════╣");
        Console.WriteLine($"║ INVENTORY ({c.Inventory.Items.Count} stacks, {c.Inventory.CurrentWeight:F1}kg):");
        foreach (var item in c.Inventory.Items.Take(5))
        {
            string stackInfo = item is Game.Core.Items.Types.Consumable con ? $" x{con.StackSize}" : "";
            Console.WriteLine($"║   - {item.Name}{stackInfo} ({item.Weight:F1}kg)");
        }
        if (c.Inventory.Items.Count > 5) Console.WriteLine($"║   ... and {c.Inventory.Items.Count - 5} more");
        Console.WriteLine("╚══════════════════════════════════════════╝");
    }
}