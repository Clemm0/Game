namespace Game.Core.Items.Types.Armors;

public class GauntletsPiece : ArmorPiece
{
    public GauntletsPiece(string setName = "Generic")
    {
        Slot = ArmorSlot.Gauntlets;
        SetName = setName;
        Name = "Gauntlets";
        BaseDefense = 2f;
        Weight = 1f;
    }
}