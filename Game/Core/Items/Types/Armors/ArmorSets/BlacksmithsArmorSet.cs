using Game.Core.Items.Materials;
using Game.Core.Rpg;

namespace Game.Core.Items.Types.Armors.ArmorSets;

public class BlacksmithsArmorSet : ArmorSet
{
    public BlacksmithsArmorSet(Material material)
    {
        Name = "Blacksmith's";
        PrimaryMaterial = material;

        AddPiece(new HelmetPiece(Name), plateCount: 1);
        AddPiece(new ChestPiece(Name), plateCount: 2);
        AddPiece(new ArmsPiece(Name), plateCount: 2);
        AddPiece(new LegsPiece(Name), plateCount: 2);
        AddPiece(new GauntletsPiece(Name), plateCount: 1);

        foreach (var p in Pieces.Values) p.Rarity = Rarity.Uncommon;

        ArmorSetRegistry.Register(Name, 5, () => 
        {
            var s = new StatBlock();
            s.MagicResist.PercentBonus += 0.10f; 
            return s;
        });
    }

    public override StatBlock? SetBonus() => null;
}