namespace Game.Core.Items.Types.Armors;

public class FeetPiece : ArmorPiece
{
    public FeetPiece(string setName = "Generic")
    {
        Slot = ArmorSlot.Feet;
        SetName = setName;
        Name = "Feet";
        BaseDefense = 2f;
        Weight = 1f;
    }
}