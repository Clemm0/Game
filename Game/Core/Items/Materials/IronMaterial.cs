using System.Collections.Generic;

namespace Game.Core.Items.Materials;

public class IronMaterial : Material
{
    public IronMaterial()
    {
        Name = "Iron";
        WeightPerUnit = 2.5f;
        Rarity = Rarity.Common;
        Type = MaterialType.Iron;
        Traits.AddRange(new[]
        {
            MaterialTrait.Of(MaterialTraitKind.DurabilityBonus, 0.10f),
            MaterialTrait.Of(MaterialTraitKind.DefenseBonus,    0.05f),
        });
    }
}