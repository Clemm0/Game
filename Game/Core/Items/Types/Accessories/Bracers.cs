using Game.Core.Rpg;

namespace Game.Core.Items.Types.Accessories;

public class Bracers : Types.Accessory
{
    public Bracers() { Name = "Bracers"; }

    public override StatBlock ResolveStats()
    {
        var stats = new StatBlock();
        var mult = RarityTable.Data[Rarity].MainStatMultiplier * Quality * StatMultiplier;
        stats.Defense.BaseValue += 2f * mult;
        stats.Primary[PrimaryStat.STR].FlatBonus += 1f * mult;
        return ItemAggregator.Apply(this, stats);
    }
}