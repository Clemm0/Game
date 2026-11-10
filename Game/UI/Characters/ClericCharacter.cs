using System.Collections.Generic;
using Game.Core.Items;
using Game.Core.Items.Materials;
using Game.Core.Items.Parts;
using Game.Core.Items.Types.Armors;
using Game.Core.Items.Types.Armors.ArmorSets;
using Game.Core.Items.Types.Weapons.Magic;
using Game.Core.Items.Types.Consumables;
using Game.Core.Rpg;
using Game.Core.Rpg.Classes.Cleric;
using Game.Core.Rpg.Equipment;
using Game.Core.Rpg.Races;
using Game.Core.Crafting;

namespace Game.UI.Characters;

public class ClericCharacter : Character
{
    public ClericCharacter()
    {
        Name = "Sister Faith";
        Race = new CommonerRace();
        Class = new ClericClass();

        BaseStats.Health.BaseValue = 50f;
        Health = 50f;
        BaseStats.Mana.BaseValue = 40f;
        Mana = 40f;
        BaseStats.Primary[PrimaryStat.INT].BaseValue = 4f;
        BaseStats.Primary[PrimaryStat.AGL].BaseValue = 2f;
        BaseStats.Defense.BaseValue = 12f;

        Class.Skills.Add(new Heal());

        Class.PreferredWeapons.Add("Spellbook");
        Class.PreferredWeapons.Add("Staff");

        var light = new LightArmorSet(new SteelMaterial());
        Equipment.Equip(EquipmentSlot.Head, light.Pieces[ArmorSlot.Helmet]);
        Equipment.Equip(EquipmentSlot.Chest, light.Pieces[ArmorSlot.Chest]);
        Equipment.Equip(EquipmentSlot.Arms, light.Pieces[ArmorSlot.Arms]);
        Equipment.Equip(EquipmentSlot.Legs, light.Pieces[ArmorSlot.Legs]);
        Equipment.Equip(EquipmentSlot.Hands, light.Pieces[ArmorSlot.Gauntlets]);

        var notebook = new Spellbook { Name = "Holy Codex" };
        ItemAssembler.Assemble(notebook, new List<ItemPart> { new NotebookPart(new SteelMaterial()) }, 1.0f);
        Equipment.Equip(EquipmentSlot.OffHand, notebook);

        Inventory.TryAddItem(new HealthPotion(20f) { StackSize = 3 });
    }
}