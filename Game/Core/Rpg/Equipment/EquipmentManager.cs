using System.Collections.Generic;
using System.Linq;
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
        if (!IsValidSlot(slot, item)) return false;

        if (_equippedItems.ContainsKey(slot))
            Unequip(slot);

        _equippedItems[slot] = item;
        return true;
    }

    public bool Unequip(EquipmentSlot slot) => _equippedItems.Remove(slot);

    public bool TrySwapEquipment(EquipmentSlot slot, Item newItem, Character owner)
    {
        if (newItem == null) return false;
        if (!IsValidSlot(slot, newItem)) return false;
        if (!owner.Inventory.Items.Contains(newItem))
        {
            System.Console.WriteLine($"  {newItem.Name} is not in {owner.Name}'s inventory.");
            return false;
        }

        Item? previousItem = null;
        if (_equippedItems.ContainsKey(slot))
        {
            previousItem = _equippedItems[slot];
        }

        owner.Inventory.TryRemoveItem(newItem);

        _equippedItems[slot] = newItem;

        if (previousItem != null)
        {
            if (!owner.Inventory.TryAddItem(previousItem))
            {
                _equippedItems[slot] = previousItem!;
                owner.Inventory.TryAddItem(newItem);
                System.Console.WriteLine($"  Swap failed — inventory is too full to hold the old {previousItem.Name}.");
                return false;
            }
            System.Console.WriteLine($"  Unequipped {previousItem.Name}, equipped {newItem.Name}.");
        }
        else
        {
            System.Console.WriteLine($"  Equipped {newItem.Name}.");
        }

        return true;
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