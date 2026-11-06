using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core.Items;
using Game.Core.Items.Materials;
using Game.Core.Items.Parts;

namespace Game.Core.Crafting;

public static class ItemAssembler
{
    public static AssemblyResult Assemble(
        Item baseItem, 
        IEnumerable<ItemPart> parts, 
        float quality = 1.0f, 
        ItemModifier modifier = ItemModifier.Pristine)
    {
        if (baseItem == null) return new AssemblyResult(null!, false, "Base item is missing.");

        int partsAttached = 0;

        foreach (var part in parts)
        {
            if (!part.Fits(baseItem))
            {
                return new AssemblyResult(null!, false, $"The part '{part.Name}' does not fit on a '{baseItem.Name}'.");
            }

            if (part.Material != null)
            {
                baseItem.Composition.AddPart(part, part.Material);
                partsAttached++;
            }
            else
            {
                return new AssemblyResult(null!, false, $"The part '{part.Name}' is missing a material.");
            }
        }

        if (partsAttached == 0)
        {
            return new AssemblyResult(null!, false, "Cannot assemble an item with no valid parts.");
        }

        baseItem.Quality = quality;
        baseItem.Modifier = modifier;

        baseItem.RebuildPartId();

        return new AssemblyResult(baseItem, true, $"Successfully assembled {baseItem.Name}!");
    }
}