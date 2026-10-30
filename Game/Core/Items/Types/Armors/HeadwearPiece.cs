namespace Game.Core.Items.Types.Armors;

public class HeadwearPiece : ArmorPiece
{
    public HeadwearPiece(string setName = "Generic", string kind = "Cowl")
    {
        Slot = ArmorSlot.Headwear;
        SetName = setName;
        Name = kind;
        BaseDefense = 1f;
        Weight = 1f;
    }
}