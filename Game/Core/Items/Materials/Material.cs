using System.Collections.Generic;
using Game.Core.Rpg;

namespace Game.Core.Items.Materials;

public abstract class Material : Item
{
    public float WeightPerUnit { get; protected set; }

    public List<MaterialTrait> Traits { get; } = new();

    public override StatBlock ResolveStats() => new();
}