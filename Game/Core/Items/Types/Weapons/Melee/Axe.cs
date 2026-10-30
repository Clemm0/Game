using Game.Core.Rpg;

namespace Game.Core.Items.Types.Weapons.Melee;

public class Axe : Types.Weapon
{
    public Axe()
    {
        Name = "Axe";
        BaseDamage = 15f;
        AttackSpeed = 0.8f;
    }

    public override StatBlock ResolveStats()
    {
        var stats = new StatBlock();
        var mult = RarityTable.Data[Rarity].MainStatMultiplier * Quality;
        stats.AttackPower.BaseValue = BaseDamage * mult;
        stats.CritChance.FlatBonus += 0.03f;
        return ItemAggregator.Apply(this, stats);
    }
}