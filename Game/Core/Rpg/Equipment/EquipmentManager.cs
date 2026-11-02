using System.Collections.Generic;
using Game.Core.Items;
using Game.Core.Items.Types;
using Game.Core.Items.Types.Armors;
using Game.Core.Items.Types.Accessories;

namespace Game.Core.Rpg.Equipment;

public class EquipmentManager
{
    private readonly Dictionary<EquipmentSlot, Item> _equippedItems = new();

    public IReadOnlyDictionary<EquipmentSlot, Item> EquippedItems => _equippedItems;

    public bool Equip(EquipmentSlot slot, Item item)
    {
        if (item == null) return false;

        if (!IsValidSlot(slot, item))
            return false;

        if (_equippedItems.ContainsKey(slot))
        {
            Unequip(slot);
        }

        _equippedItems[slot] = item;
        return true;
    }

    public bool Unequip(EquipmentSlot slot)
    {
        return _equippedItems.Remove(slot);
    }

        private bool IsValidSlot(EquipmentSlot slot, Item item)
    {
        return slot switch
        {
            EquipmentSlot.MainHand or EquipmentSlot.OffHand => item is Weapon,
            EquipmentSlot.Head => item is HelmetPiece or HeadwearPiece,
            EquipmentSlot.Chest => item is ChestPiece,
            EquipmentSlot.Arms => item is ArmsPiece,
            EquipmentSlot.Legs => item is LegsPiece,
            EquipmentSlot.Hands => item is GauntletsPiece,
            EquipmentSlot.Feet => item is FeetPiece,
            EquipmentSlot.Ring1 or EquipmentSlot.Ring2 => item is Ring,
            EquipmentSlot.Neck => item is Amulet,
            _ => false
        };
    }
}