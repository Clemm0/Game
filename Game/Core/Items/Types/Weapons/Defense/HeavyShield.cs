using Game.Core.Rpg;

namespace Game.Core.Items.Types.Weapons.Defense;

public class HeavyShield : Types.Weapon
{
    public HeavyShield()
    {
        Name = "Heavy Shield";
        BaseDamage = 4f;
        AttackSpeed = 0.5f;
    }

    public override StatBlock ResolveStats()
    {
        var stats = new StatBlock();
        var mult = RarityTable.Data[Rarity].MainStatMultiplier * Quality;
        stats.Defense.BaseValue = 20f * mult;
        stats.MoveSpeed.PercentBonus -= 0.15f;
        return ItemAggregator.Apply(this, stats);
    }
}