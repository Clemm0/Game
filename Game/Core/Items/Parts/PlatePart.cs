using Game.Core.Items.Materials;

namespace Game.Core.Items.Parts;

public class PlatePart : ItemPart
{
    public PlatePart(Material material)
    {
        Name = "Plate";
        Material = material;
        MaterialUnits = 4;
        Type = PartType.Plate;
    }

    public override bool Fits(Item host) => host is Types.Armor;
}