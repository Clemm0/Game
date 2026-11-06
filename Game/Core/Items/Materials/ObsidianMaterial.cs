namespace Game.Core.Items.Materials;

public class ObsidianMaterial : Material
{
    public ObsidianMaterial()
    {
        Name = "Obsidian";
        WeightPerUnit = 2.8f;
        Rarity = Rarity.Rare;
        Type = MaterialType.Obsidian;
        Traits.AddRange(new[]
        {
            MaterialTrait.Of(MaterialTraitKind.FireAffinity,    0.25f, "Fire"),
            MaterialTrait.Of(MaterialTraitKind.DamageBonus,     0.20f),
            MaterialTrait.Of(MaterialTraitKind.DurabilityBonus, -0.15f),
        });
    }
}