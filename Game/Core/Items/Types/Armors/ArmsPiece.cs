namespace Game.Core.Items.Types.Armors;

public class ArmsPiece : ArmorPiece
{
    public ArmsPiece(string setName = "Generic")
    {
        Slot = ArmorSlot.Arms;
        SetName = setName;
        Name = "Arms";
        BaseDefense = 4f;
        Weight = 3f;
    }
}