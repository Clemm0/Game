using Game.Core.Items.Parts;
using Game.Core.Items.Materials;
using Game.Core.Rpg;

namespace Game.Core.Items.Types.Weapons.Melee;

public class Dagger : Weapon
{
    public Dagger()
    {
        Name = "Dagger";
        BaseDamage = 8f;
        AttackSpeed = 1.4f;
    }

    public override StatBlock ResolveStats()
    {
        var stats = new StatBlock();
        var rarityMult = RarityTable.Data[Rarity].MainStatMultiplier * Quality;
        stats.AttackPower.BaseValue = BaseDamage * rarityMult;

        return ItemAggregator.Apply(this, stats);
    }
}