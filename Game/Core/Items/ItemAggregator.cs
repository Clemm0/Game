using Game.Core.Items;
using Game.Core.Items.Materials;
using Game.Core.Rpg;

namespace Game.Core.Items;

public static class ItemAggregator
{
    public static StatBlock Apply(Item item, StatBlock baseStats)
    {
        foreach (var trait in item.Composition.AllTraits())
        {
            switch (trait.Kind)
            {
                case MaterialTraitKind.DamageBonus:
                    baseStats.AttackPower.PercentBonus += trait.Magnitude;
                    break;
                case MaterialTraitKind.AttackSpeedBonus:
                    break;
                case MaterialTraitKind.CritChanceBonus:
                    baseStats.CritChance.FlatBonus += trait.Magnitude;
                    break;
                case MaterialTraitKind.DefenseBonus:
                    baseStats.Defense.PercentBonus += trait.Magnitude;
                    break;
                case MaterialTraitKind.DurabilityBonus:
                    break;
                case MaterialTraitKind.WeightReduction:
                    break;
            }
        }
        return baseStats;
    }
}