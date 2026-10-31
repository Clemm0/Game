using Game.Core.Items;
using Game.Core.Items.Materials;
using Game.Core.Items.Parts;
using Game.Core.Items.Types.Weapons.Melee;

namespace Game.Core.Crafting;

public static class DaggerFactory
{
    public static Dagger Craft(Material bladeMaterial, Rarity rarity = Rarity.Common)
    {
        var dagger = new Dagger { Rarity = rarity };

        var blade = new BladePart(bladeMaterial);
        dagger.Composition.AddPart(blade, bladeMaterial);

        return dagger;
    }
}