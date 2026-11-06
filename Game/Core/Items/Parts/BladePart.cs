using Game.Core.Items.Materials;

namespace Game.Core.Items.Parts;

public class BladePart : ItemPart
{
    public BladePart(Material material)
    {
        Name = "Blade";
        Material = material;
        Type = PartType.Blade;
    }

    public override bool Fits(Item host) => host is Types.Weapon;
}