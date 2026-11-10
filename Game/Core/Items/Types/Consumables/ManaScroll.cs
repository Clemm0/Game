using System;
using Game.Core.Rpg;

namespace Game.Core.Items.Types.Consumables;

public class ManaScroll : Consumable
{
    public float RestoreAmount { get; protected set; }

    public ManaScroll(float restoreAmount = 20f)
    {
        Name = "Mana Scroll";
        Rarity = Rarity.Uncommon;
        Quality = 1.0f;
        RestoreAmount = restoreAmount;
        MaxStackSize = 3;
    }

    public override void ApplyEffect(Character target)
    {
        var stats = target.GetEffectiveStats();
        float maxMana = stats.Mana.Effective;
        
        target.Mana = Math.Min(target.Mana + RestoreAmount, maxMana);
        Console.WriteLine($"  *Reads {Name}* (Restored {RestoreAmount} MP. Current MP: {target.Mana:F1}/{maxMana:F1})");
    }

    public override StatBlock ResolveStats() => new StatBlock();
}