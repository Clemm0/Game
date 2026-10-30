using Game.Core.Items.Materials;

namespace Game.Core.Items.Parts;

public class HiltPart : ItemPart
{
    public HiltPart(Material material)
    {
        Name = "Hilt";
        Material = material;
        MaterialUnits = 1;
    }

    public override bool Fits(Item host) => host is Types.Weapon;
}