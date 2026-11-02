using Game.Core.Items.Materials;
using Game.Core.Items.Parts;
using Game.Core.Rpg;

namespace Game.Core.Items.Types.Armors;

public abstract class ArmorPiece : Armor
{
    public ArmorSlot Slot { get; protected set; }
    public string SetName { get; protected set; } = "Generic";

    public override StatBlock ResolveStats()
    {
        var stats = new StatBlock();
        var mult = RarityTable.Data[Rarity].MainStatMultiplier * Quality;
        
        float totalDefense = BaseDefense;
        
        foreach (var slot in Composition.Slots)
        {
            if (slot.Part is PlatePart)
            {
                totalDefense += 2f;
            }
        }

        stats.Defense.BaseValue = totalDefense * mult;
        return ItemAggregator.Apply(this, stats);
    }

    internal void BuildWith(Material material, int plateCount)
    {
        for (int i = 0; i < plateCount; i++)
            Composition.AddPart(new PlatePart(material), material);
    }
}