using System.Collections.Generic;

namespace Game.Core.Items.Materials;

public abstract class Material : Item
{
    public float WeightPerUnit { get; protected set; }

    public List<MaterialTrait> Traits { get; } = new();

    public override StatBlock ResolveStats() => new();
}