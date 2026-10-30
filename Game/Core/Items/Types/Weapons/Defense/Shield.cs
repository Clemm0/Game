using Game.Core.Rpg;

namespace Game.Core.Items.Types.Weapons.Defense;

public class Shield : Types.Weapon
{
    public Shield()
    {
        Name = "Shield";
        BaseDamage = 2f;
        AttackSpeed = 0.8f;
    }

    public override StatBlock ResolveStats()
    {
        var stats = new StatBlock();
        var mult = RarityTable.Data[Rarity].MainStatMultiplier * Quality;
        stats.Defense.BaseValue = 10f * mult;
        return ItemAggregator.Apply(this, stats);
    }
}