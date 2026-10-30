namespace Game.Core.Items.Types.Armors;

public class LegsPiece : ArmorPiece
{
    public LegsPiece(string setName = "Generic")
    {
        Slot = ArmorSlot.Legs;
        SetName = setName;
        Name = "Legs";
        BaseDefense = 5f;
        Weight = 4f;
    }
}