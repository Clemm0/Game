using Game.Core.Rpg;

namespace Game.Core.Items.Types.Weapons.Magic;

public class Scroll : Types.Weapon
{
    public Scroll()
    {
        Name = "Scroll";
        BaseDamage = 0f;
        AttackSpeed = 1f;
    }

    public override StatBlock ResolveStats()
    {
        var stats = new StatBlock();
        var mult = RarityTable.Data[Rarity].MainStatMultiplier * Quality;
        stats.SpellPower.BaseValue = 8f * mult;
        return ItemAggregator.Apply(this, stats);
    }
}