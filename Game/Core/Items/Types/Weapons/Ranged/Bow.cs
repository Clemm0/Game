using Game.Core.Rpg;

namespace Game.Core.Items.Types.Weapons.Ranged;

public class Bow : Types.Weapon
{
    public Bow()
    {
        Name = "Bow";
        BaseDamage = 10f;
        AttackSpeed = 0.9f;
    }

    public override StatBlock ResolveStats()
    {
        var stats = new StatBlock();
        var mult = RarityTable.Data[Rarity].MainStatMultiplier * Quality;
        stats.AttackPower.BaseValue = BaseDamage * mult;
        return ItemAggregator.Apply(this, stats);
    }
}