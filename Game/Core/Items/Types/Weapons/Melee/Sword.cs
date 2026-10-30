using Game.Core.Rpg;

namespace Game.Core.Items.Types.Weapons.Melee;

public class Sword : Types.Weapon
{
    public Sword()
    {
        Name = "Sword";
        BaseDamage = 12f;
        AttackSpeed = 1.0f;
    }

    public override StatBlock ResolveStats()
    {
        var stats = new StatBlock();
        var mult = RarityTable.Data[Rarity].MainStatMultiplier * Quality;
        stats.AttackPower.BaseValue = BaseDamage * mult;
        return ItemAggregator.Apply(this, stats);
    }
}