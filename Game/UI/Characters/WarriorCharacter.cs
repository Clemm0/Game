using System.Collections.Generic;
using Game.Core.Items;
using Game.Core.Items.Materials;
using Game.Core.Items.Parts;
using Game.Core.Items.Types.Armors;
using Game.Core.Items.Types.Armors.ArmorSets;
using Game.Core.Items.Types.Weapons.Defense;
using Game.Core.Items.Types.Weapons.Melee;
using Game.Core.Items.Types.Consumables;
using Game.Core.Rpg;
using Game.Core.Rpg.Classes.Warrior;
using Game.Core.Rpg.Equipment;
using Game.Core.Rpg.Races;
using Game.Core.Crafting;

namespace Game.UI.Characters;

public class WarriorCharacter : Character
{
    public WarriorCharacter()
    {
        Name = "Brutus";
        Race = new CommonerRace();
        Class = new WarriorClass();

        BaseStats.Health.BaseValue = 70f;
        Health = 70f;
        BaseStats.Primary[PrimaryStat.STR].BaseValue = 4f;
        BaseStats.Primary[PrimaryStat.AGL].BaseValue = 1f;
        BaseStats.Defense.BaseValue = 14f;

        Class.Skills.Add(new Taunt());
        Class.Skills.Add(new Cleave());

        Class.PreferredWeapons.Add("Sword");
        Class.PreferredWeapons.Add("Shield");
        Class.PreferredWeapons.Add("Axe");
        Class.PreferredWeapons.Add("Pike");

        Proficiencies.Add("Swords");
        BaseStats.Skills["Swords"] = 2;

        var heavy = new HeavyArmorSet(new IronMaterial());
        Equipment.Equip(EquipmentSlot.Head, heavy.Pieces[ArmorSlot.Helmet]);
        Equipment.Equip(EquipmentSlot.Chest, heavy.Pieces[ArmorSlot.Chest]);
        Equipment.Equip(EquipmentSlot.Arms, heavy.Pieces[ArmorSlot.Arms]);
        Equipment.Equip(EquipmentSlot.Legs, heavy.Pieces[ArmorSlot.Legs]);
        Equipment.Equip(EquipmentSlot.Hands, heavy.Pieces[ArmorSlot.Gauntlets]);

        var shield = new Shield { Name = "Iron Shield" };
        ItemAssembler.Assemble(shield, new List<ItemPart> { new PlatePart(new IronMaterial()) }, 1.0f);
        Equipment.Equip(EquipmentSlot.OffHand, shield);

        Inventory.TryAddItem(new HealthPotion(25f) { StackSize = 2 });
    }
}