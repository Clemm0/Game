using Game.Core.Items.Materials;
using Game.Core.Rpg;

namespace Game.Core.Items.Types.Armors.ArmorSets;

public class RobeArmorSet : ArmorSet
{
    public RobeArmorSet(Material material, string headKind = "Cowl")
    {
        Name = "Robe";
        PrimaryMaterial = material;

        AddPiece(new HeadwearPiece(Name, headKind), plateCount: 0);
        AddPiece(new ChestPiece(Name), plateCount: 1);
        AddPiece(new ArmsPiece(Name), plateCount: 1);
        AddPiece(new LegsPiece(Name), plateCount: 1);
    }

    public override StatBlock? SetBonus()
    {
        var s = new StatBlock();
        s.SpellPower.PercentBonus += 0.08f;
        s.Mana.BaseValue += 15f;
        return s;
    }
}