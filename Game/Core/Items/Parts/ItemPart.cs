using Game.Core.Items.Materials;
using Game.Core.Rpg;

namespace Game.Core.Items.Parts;

public abstract class ItemPart : Item
{
    public Material? Material { get; protected set; }
    public int MaterialUnits { get; protected set; } = 1;

    public abstract bool Fits(Item host);
    public override StatBlock ResolveStats() => new();
}