using System;
using System.Linq;
using System.Text;
using Game.Core.Items.Parts;
using Game.Core.Items.Materials;
using Game.Core.Rpg;

namespace Game.Core.Items;

public abstract class Item
{
    public string Id { get; protected set; }
    
    public string TemplateId { get; internal set; } = string.Empty;
    public string Name { get; internal set; } = string.Empty;
    public Rarity Rarity { get; internal set; } = Rarity.Common;
    public float Quality { get; internal set; }
    public ItemModifier Modifier { get; internal set; } = ItemModifier.Used;
    
    public virtual float Weight => Composition.TotalWeight(); 

    public Composition Composition { get; } = new();

    protected Item()
    {
        Id = Guid.NewGuid().ToString("N").Substring(0, 8);
    }

    public void RebuildPartId()
    {
        var baseId = Id.Split('-')[0]; 
        
        var slots = Composition.Slots.ToList();
        
        var partHexes = new string[4];
        
        for (int i = 0; i < 4; i++)
        {
            if (i < slots.Count)
            {
                var part = slots[i];
                partHexes[i] = $"{(byte)part.Part.Type:X2}{(byte)part.Material.Type:X2}";
            }
            else
            {
                partHexes[i] = "0000";
            }
        }

        Id = $"{baseId}-{partHexes[0]}-{partHexes[1]}-{partHexes[2]}-{partHexes[3]}";
    }

    public abstract StatBlock ResolveStats();

    public virtual void Degrade(float amount)
        => Quality = System.Math.Clamp(Quality - amount, 0f, 1f);

    protected void AttachPart(ItemPart part, Material material)
        => Composition.AddPart(part, material);
}