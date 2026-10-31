using System.Collections.Generic;
using Game.Core.Items.Parts;
using Game.Core.Items.Materials;

namespace Game.Core.Items;

public class Composition
{
    private readonly List<ComponentSlot> _slots = new();

    public IReadOnlyList<ComponentSlot> Slots => _slots;

    public void AddPart(ItemPart part, Material material)
    {
        _slots.Add(new ComponentSlot(part, material));
    }

    public IEnumerable<MaterialTrait> AllTraits()
    {
        foreach (var slot in _slots)
            foreach (var trait in slot.Material.Traits)
                yield return trait;
    }

    public float TotalWeight()
    {
        float total = 0f;
        foreach (var slot in _slots)
        {
            total += slot.Material.WeightPerUnit * slot.Part.MaterialUnits;
        }
        return total;
    }

    public readonly record struct ComponentSlot(ItemPart Part, Material Material);
}