using System.Collections.Generic;
using System.Linq;
using Game.Core.Items;
using Game.Core.Rpg;

namespace Game.Core.Inventory;

public class Inventory
{
    private readonly List<Item> _items = new();
    
    public IReadOnlyList<Item> Items => _items.AsReadOnly();

    public float MaxCapacity { get; set; } = 50f;
    
    public float CurrentWeight => _items.Sum(i => i.Weight);

    private bool _isPickupLocked = false;

    public EncumbranceState State
    {
        get
        {
            float ratio = CurrentWeight / MaxCapacity;
            if (ratio <= 0.5f) return EncumbranceState.Free;
            if (ratio <= 0.9f) return EncumbranceState.Stacked;
            if (ratio <= 1.0f) return EncumbranceState.LightlyOver;
            return EncumbranceState.Overencumbered;
        }
    }

    public bool TryAddItem(Item item)
    {
        UpdateLockStatus();

        if (_isPickupLocked)
        {
            return false; 
        }

       if (CurrentWeight + item.Weight > MaxCapacity)
        {
            return false;
        }
        
        _items.Add(item);
        UpdateLockStatus();
        return true;
    }

    public bool TryRemoveItem(Item item)
    {
        bool removed = _items.Remove(item);
        if (removed)
        {
            UpdateLockStatus();
        }
        return removed;
    }

    public void UpdateLockStatus()
    {
        float ratio = CurrentWeight / MaxCapacity;

        if (ratio >= 1.0f)
        {
            _isPickupLocked = true;
        }
        else if (ratio <= 0.99f)
        {
            _isPickupLocked = false;
        }
    }

    public void ApplyEncumbrancePenalties(Character character, StatBlock effectiveStats)
    {
        switch (State)
        {
            case EncumbranceState.Free:
                break;
            case EncumbranceState.Stacked:
                effectiveStats.MoveSpeed.PercentBonus -= 0.05f; 
                break;
            case EncumbranceState.LightlyOver:
                effectiveStats.MoveSpeed.PercentBonus -= 0.25f; 
                break;
            case EncumbranceState.Overencumbered:
                effectiveStats.MoveSpeed.PercentBonus -= 0.50f; 
                break;
        }
    }
}