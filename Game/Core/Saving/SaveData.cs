using System.Collections.Generic;

namespace Game.Core.Saving;

public class SaveData
{
    public string PlayerName { get; set; } = "Hero";
    public int Level { get; set; } = 1;
    public int Experience { get; set; } = 0;
    public int AvailablePerkPoints { get; set; }
    public int AvailableSpecialPoints { get; set; }
    
    public float Health { get; set; }
    public float Mana { get; set; }
    public float Stamina { get; set; }

    public float BaseHealth { get; set; }
    public float BaseMana { get; set; }
    public float BaseStamina { get; set; }
    public float BaseDefense { get; set; }
    public float BaseAttackPower { get; set; }
    public float BaseSpellPower { get; set; }
    public float BaseMoveSpeed { get; set; }
    public float BaseCritChance { get; set; }

    public Dictionary<string, float> PrimaryStats { get; set; } = new();
    public Dictionary<string, int> Skills { get; set; } = new();
    
    public List<ItemSaveData> EquippedItems { get; set; } = new();
    public List<ItemSaveData> InventoryItems { get; set; } = new();
}

public class ItemSaveData
{
    public string TypeName { get; set; } = ""; 
    public string Slot { get; set; } = "";     
    public string Name { get; set; } = "";
    public float Quality { get; set; }
    public string Rarity { get; set; } = "";
    public string Modifier { get; set; } = "";
    public int StackSize { get; set; } = 1;
    public List<PartSaveData> Parts { get; set; } = new();
}

public class PartSaveData
{
    public string PartType { get; set; } = "";
    public string MaterialType { get; set; } = "";
}