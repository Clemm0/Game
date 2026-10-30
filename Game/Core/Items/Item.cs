using Game.Core.Items.Parts;
using Game.Core.Items.Materials;
using Game.Core.Rpg;

namespace Game.Core.Items;

public abstract class Item
{
    public string Name { get; protected set; } = string.Empty;
    public Rarity Rarity { get; protected set; } = Rarity.Common;
    public float Quality { get; protected set; }
    public ItemModifier Modifier { get; protected set; } = ItemModifier.Used;

    public Composition Composition { get; } = new();

    public abstract StatBlock ResolveStats();

    public virtual void Degrade(float amount)
        => Quality = System.Math.Clamp(Quality - amount, 0f, 1f);

    protected void AttachPart(ItemPart part, Material material)
        => Composition.AddPart(part, material);
}