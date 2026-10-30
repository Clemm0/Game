using Game.Core.Items.Materials;
using Game.Core.Rpg;

namespace Game.Core.Items.Types.Armors.ArmorSets;

public class HeavyArmorSet : ArmorSet
{
    public HeavyArmorSet(Material material)
    {
        Name = "Heavy Armor";
        PrimaryMaterial = material;

        AddPiece(new HelmetPiece(Name), plateCount: 2);
        AddPiece(new ChestPiece(Name), plateCount: 4);
        AddPiece(new ArmsPiece(Name), plateCount: 3);
        AddPiece(new LegsPiece(Name), plateCount: 3);
        AddPiece(new GauntletsPiece(Name), plateCount: 1);

        foreach (var p in Pieces.Values) p.Rarity = Rarity.Rare;
    }

    public override StatBlock? SetBonus()
    {
        var s = new StatBlock();
        s.Defense.PercentBonus += 0.10f;
        s.MoveSpeed.PercentBonus -= 0.05f;
        return s;
    }
}