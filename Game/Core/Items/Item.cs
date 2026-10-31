using System;
using Game.Core.Items.Parts;
using Game.Core.Items.Materials;
using Game.Core.Rpg;

namespace Game.Core.Items;

public abstract class Item
{
    public string Id { get; protected set; } = Guid.NewGuid().ToString();
    public string TemplateId { get; internal set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Rarity Rarity { get; internal set; } = Rarity.Common;
    public float Quality { get; internal set; }
    public ItemModifier Modifier { get; internal set; }
    
    public virtual float Weight => Composition.TotalWeight(); 

    public Composition Composition { get; } = new();

    public abstract StatBlock ResolveStats();

    public virtual void Degrade(float amount)
        => Quality = System.Math.Clamp(Quality - amount, 0f, 1f);
}