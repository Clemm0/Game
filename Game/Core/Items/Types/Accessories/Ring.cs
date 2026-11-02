using Game.Core.Rpg;

namespace Game.Core.Items.Types.Accessories;

public class Ring : Types.Accessory
{
    public Ring() { Name = "Ring"; }

    public override StatBlock ResolveStats()
    {
        var stats = new StatBlock();
        var mult = RarityTable.Data[Rarity].MainStatMultiplier * Quality * StatMultiplier;
        stats.Primary[PrimaryStat.INT].FlatBonus += 3f * mult;
        return ItemAggregator.Apply(this, stats);
    }
}