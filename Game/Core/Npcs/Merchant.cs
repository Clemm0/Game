using System;
using Game.Core.Items;
using Game.Core.Rpg;
using Game.Core.Generic;

namespace Game.Core.Npcs;

public class Merchant : GenericEntity
{
    public Game.Core.Inventory.Inventory Inventory { get; } = new() { MaxCapacity = 9999f };
    
    public float Gold { get; private set; } = 500f;

    public Merchant(string name = "Traveling Merchant") 
    {
        Name = name;
    }

    public override void Update(float deltaTime) {}

    public bool BuyFromPlayer(Character player, Item item, float price)
    {
        if (player.Inventory != null && player.Inventory.TryRemoveItem(item))
        {
            Gold -= price;
            Inventory.TryAddItem(item);
            return true;
        }
        return false;
    }
}