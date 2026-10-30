using Game.Core.Rpg;

namespace Game.Core.Items.Types.Weapons.Magic;

public class Staff : Types.Weapon
{
    public Staff()
    {
        Name = "Staff";
        BaseDamage = 6f;
        AttackSpeed = 0.7f;
    }

    public override StatBlock ResolveStats()
    {
        var stats = new StatBlock();
        var mult = RarityTable.Data[Rarity].MainStatMultiplier * Quality;
        stats.SpellPower.BaseValue = 12f * mult;
        return ItemAggregator.Apply(this, stats);
    }
}