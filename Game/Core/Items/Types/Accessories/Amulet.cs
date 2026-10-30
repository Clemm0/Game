using Game.Core.Rpg;

namespace Game.Core.Items.Types.Accessories;

public class Amulet : Types.Accessory
{
    public Amulet() { Name = "Amulet"; }

    public override StatBlock ResolveStats()
    {
        var stats = new StatBlock();
        var mult = RarityTable.Data[Rarity].MainStatMultiplier * Quality * StatMultiplier;
        stats.MagicResist.PercentBonus += 0.05f * mult;
        stats.Mana.BaseValue += 10f * mult;
        return ItemAggregator.Apply(this, stats);
    }
}