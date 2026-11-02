using Game.Core.Items.Materials;
using Game.Core.Rpg;

namespace Game.Core.Items.Types.Armors.ArmorSets;

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

        ArmorSetRegistry.Register(Name, 4, () => 
        {
            var s = new StatBlock();
            s.Primary[PrimaryStat.CHA].BaseValue += 5f;
            return s;
        });
    }

    public override StatBlock? SetBonus() => null;
}