using Game.Core.Rpg;

namespace Game.Core.Items.Types.Weapons.Ranged;

public class Crossbow : Types.Weapon
{
    public Crossbow()
    {
        Name = "Crossbow";
        BaseDamage = 14f;
        AttackSpeed = 0.6f;
    }

    public override StatBlock ResolveStats()
    {
        var stats = new StatBlock();
        var mult = RarityTable.Data[Rarity].MainStatMultiplier * Quality;
        stats.AttackPower.BaseValue = BaseDamage * mult;
        return ItemAggregator.Apply(this, stats);
    }
}