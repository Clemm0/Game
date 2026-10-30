using Game.Core.Rpg;

namespace Game.Core.Items.Types.Weapons.Magic;

public class Spellbook : Types.Weapon
{
    public Spellbook()
    {
        Name = "Spellbook";
        BaseDamage = 0f;
        AttackSpeed = 1f;
    }

    public override StatBlock ResolveStats()
    {
        var stats = new StatBlock();
        var mult = RarityTable.Data[Rarity].MainStatMultiplier * Quality;
        stats.SpellPower.BaseValue = 18f * mult;
        return ItemAggregator.Apply(this, stats);
    }
}