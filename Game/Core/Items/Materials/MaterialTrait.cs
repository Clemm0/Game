namespace Game.Core.Items.Materials;

public class MaterialTrait
{
    public MaterialTraitKind Kind { get; init; }
    public float Magnitude { get; init; }
    public string? Tag { get; init; }

    public static MaterialTrait Of(MaterialTraitKind kind, float magnitude, string? tag = null)
        => new() { Kind = kind, Magnitude = magnitude, Tag = tag };
}

public enum MaterialTraitKind
{
    DamageBonus,
    AttackSpeedBonus,
    CritChanceBonus,
    ArmorPenetration,

    DefenseBonus,
    DurabilityBonus,
    WeightReduction,

    FireAffinity,
    FrostAffinity,
    ArcaneAffinity,
    PoisonAffinity,

    CraftingEfficiency,
    RepairEfficiency
}