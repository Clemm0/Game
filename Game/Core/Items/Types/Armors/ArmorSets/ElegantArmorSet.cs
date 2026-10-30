using Game.Core.Items.Materials;
using Game.Core.Rpg;

namespace Game.Core.Items.Types.Armors.ArmorSets;

/// <summary>4-slot elegant: diadem, chest, legs, feet. Social/CHA oriented.</summary>
public class ElegantArmorSet : ArmorSet
{
    public ElegantArmorSet(Material material)
    {
        Name = "Elegant";
        PrimaryMaterial = material;

        AddPiece(new HeadwearPiece(Name, "Diadem"), plateCount: 0);
        AddPiece(new ChestPiece(Name), plateCount: 1);
        AddPiece(new LegsPiece(Name), plateCount: 1);
        AddPiece(new FeetPiece(Name), plateCount: 1);

        foreach (var p in Pieces.Values) p.Rarity = Rarity.Rare;
    }

    public override StatBlock? SetBonus()
    {
        var s = new StatBlock();
        s.Primary[PrimaryStat.CHA].FlatBonus += 5f;
        return s;
    }
}