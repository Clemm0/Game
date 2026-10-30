using Game.Core.Rpg;

namespace Game.Core.Items.Types.Weapons.Melee;

public class Pike : Types.Weapon
{
    public Pike()
    {
        Name = "Pike";
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