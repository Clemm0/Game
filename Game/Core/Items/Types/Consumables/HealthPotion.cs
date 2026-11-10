using System;
using Game.Core.Rpg;

namespace Game.Core.Items.Types.Consumables;

public class HealthPotion : Consumable
{
    public float HealAmount { get; protected set; }

    public HealthPotion(float healAmount = 15f)
    {
        Name = "Health Potion";
        Rarity = Rarity.Common;
        Quality = 1.0f;
        HealAmount = healAmount;
        MaxStackSize = 5;
    }

    public override void ApplyEffect(Character target)
    {
        var stats = target.GetEffectiveStats();
        float maxHealth = stats.Health.Effective;
        
        target.Health = Math.Min(target.Health + HealAmount, maxHealth);
        Console.WriteLine($"  *Drinks {Name}* (Healed for {HealAmount} HP. Current HP: {target.Health:F1}/{maxHealth:F1})");
    }

    public override StatBlock ResolveStats() => new StatBlock();
}