using System.Collections.Generic;
using Game.Core.Items;
using Game.Core.Items.Materials;
using Game.Core.Items.Parts;
using Game.Core.Items.Types.Armors;
using Game.Core.Items.Types.Armors.ArmorSets;
using Game.Core.Items.Types.Weapons.Magic;
using Game.Core.Items.Types.Consumables;
using Game.Core.Rpg;
using Game.Core.Rpg.Classes.Mage;
using Game.Core.Rpg.Equipment;
using Game.Core.Rpg.Races;
using Game.Core.Crafting;

namespace Game.UI.Characters;

public class MageCharacter : Character
{
    public MageCharacter()
    {
        Name = "Merlin";
        Race = new CommonerRace();
        Class = new MageClass();

        BaseStats.Health.BaseValue = 40f;
        Health = 40f;
        BaseStats.Mana.BaseValue = 50f;
        Mana = 50f;
        BaseStats.Primary[PrimaryStat.INT].BaseValue = 5f;
        BaseStats.Primary[PrimaryStat.AGL].BaseValue = 3f;
        BaseStats.Defense.BaseValue = 10f;

        Class.Skills.Add(new Fireball());

        Class.PreferredWeapons.Add("Staff");
        Class.PreferredWeapons.Add("Spellbook");
        Class.PreferredWeapons.Add("Scroll");

        var robe = new RobeArmorSet(new SteelMaterial());
        Equipment.Equip(EquipmentSlot.Head, robe.Pieces[ArmorSlot.Headwear]);
        Equipment.Equip(EquipmentSlot.Chest, robe.Pieces[ArmorSlot.Chest]);
        Equipment.Equip(EquipmentSlot.Arms, robe.Pieces[ArmorSlot.Arms]);
        Equipment.Equip(EquipmentSlot.Legs, robe.Pieces[ArmorSlot.Legs]);

        var staff = new Staff { Name = "Oak Staff" };
        ItemAssembler.Assemble(staff, new List<ItemPart> { new NotebookPart(new ObsidianMaterial()) }, 1.0f);
        Equipment.Equip(EquipmentSlot.MainHand, staff);

        Inventory.TryAddItem(new HealthPotion(20f) { StackSize = 2 });
    }
}