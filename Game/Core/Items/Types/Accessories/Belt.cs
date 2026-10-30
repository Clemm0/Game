using Game.Core.Rpg;

namespace Game.Core.Items.Types.Accessories;

public class Belt : Types.Accessory
{
    public Belt() { Name = "Belt"; }

    public override StatBlock ResolveStats()
    {
        var stats = new StatBlock();
        var mult = RarityTable.Data[Rarity].MainStatMultiplier * Quality * StatMultiplier;
        stats.CarryCapacity.FlatBonus += 8f * mult;
        return ItemAggregator.Apply(this, stats);
    }
}