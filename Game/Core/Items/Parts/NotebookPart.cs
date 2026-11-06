using Game.Core.Items.Materials;

namespace Game.Core.Items.Parts;

public class NotebookPart : ItemPart
{
    public NotebookPart(Material material)
    {
        Name = "Notebook Pages";
        Material = material;
        MaterialUnits = 1;
        Type = PartType.Notebook;
    }

    public override bool Fits(Item host) => host is Types.Weapon;
}