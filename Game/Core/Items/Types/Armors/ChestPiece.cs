namespace Game.Core.Items.Types.Armors;

public class ChestPiece : ArmorPiece
{
    public ChestPiece(string setName = "Generic")
    {
        Slot = ArmorSlot.Chest;
        SetName = setName;
        Name = "Chest";
        BaseDefense = 6f;
        Weight = 5f;
    }
}