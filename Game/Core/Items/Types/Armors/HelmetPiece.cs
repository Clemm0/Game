namespace Game.Core.Items.Types.Armors;

public class HelmetPiece : ArmorPiece
{
    public HelmetPiece(string setName = "Generic")
    {
        Slot = ArmorSlot.Helmet;
        SetName = setName;
        Name = "Helmet";
        BaseDefense = 3f;
        Weight = 2f;
    }
}