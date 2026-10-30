using Game.Core.Items.Materials;
using Game.Core.Rpg;

namespace Game.Core.Items.Types.Armors.ArmorSets;

/// <summary>4-slot daily: hat, chest, legs, feet. Cheap, no combat focus.</summary>
public class DailyArmorSet : ArmorSet
{
    public DailyArmorSet(Material material)
    {
        Name = "Daily";
        PrimaryMaterial = material;

        AddPiece(new HeadwearPiece(Name, "Hat"), plateCount: 0);
        AddPiece(new ChestPiece(Name), plateCount: 1);
        AddPiece(new LegsPiece(Name), plateCount: 1);
        AddPiece(new FeetPiece(Name), plateCount: 1);
    }
}