namespace Game.Core.Items.Materials;

public class SteelMaterial : Material
{
    public SteelMaterial()
    {
        Name = "Steel";
        WeightPerUnit = 2.2f;
        Rarity = Rarity.Uncommon;
        Traits.AddRange(new[]
        {
            MaterialTrait.Of(MaterialTraitKind.DamageBonus,      0.15f),
            MaterialTrait.Of(MaterialTraitKind.CritChanceBonus,  0.05f),
            MaterialTrait.Of(MaterialTraitKind.WeightReduction,  0.05f),
        });
    }
}