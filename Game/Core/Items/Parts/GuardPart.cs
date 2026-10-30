using Game.Core.Items.Materials;

namespace Game.Core.Items.Parts;

public class GuardPart : ItemPart
{
    public GuardPart(Material material)
    {
        Name = "Guard";
        Material = material;
        MaterialUnits = 1;
    }

    public override bool Fits(Item host) => host is Types.Weapon;
}