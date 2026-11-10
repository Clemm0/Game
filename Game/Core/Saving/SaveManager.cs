using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Game.Core.Items;
using Game.Core.Items.Materials;
using Game.Core.Items.Parts;
using Game.Core.Items.Types;
using Game.Core.Items.Types.Armors;
using Game.Core.Items.Types.Accessories;
using Game.Core.Items.Types.Consumables;
using Game.Core.Items.Types.Weapons.Melee;
using Game.Core.Items.Types.Weapons.Magic;
using Game.Core.Items.Types.Weapons.Ranged;
using Game.Core.Rpg;
using Game.Core.Rpg.Equipment;

namespace Game.Core.Saving;

public static class SaveManager
{
    private static readonly JsonSerializerOptions _options = new() { WriteIndented = true };

    public static void SaveGame(Character player, string filePath)
    {
        var data = new SaveData
        {
            PlayerName = player.Name,
            Level = player.Level,
            Experience = player.Experience,
            AvailablePerkPoints = player.AvailablePerkPoints,
            AvailableSpecialPoints = player.AvailableSpecialPoints,
            Health = player.Health,
            Mana = player.Mana,
            Stamina = player.Stamina,

            BaseHealth = player.BaseStats.Health.BaseValue,
            BaseMana = player.BaseStats.Mana.BaseValue,
            BaseStamina = player.BaseStats.Stamina.BaseValue,
            BaseDefense = player.BaseStats.Defense.BaseValue,
            BaseAttackPower = player.BaseStats.AttackPower.BaseValue,
            BaseSpellPower = player.BaseStats.SpellPower.BaseValue,
            BaseMoveSpeed = player.BaseStats.MoveSpeed.BaseValue,
            BaseCritChance = player.BaseStats.CritChance.BaseValue,
        };

        foreach (var kvp in player.BaseStats.Primary)
            data.PrimaryStats[kvp.Key.ToString()] = kvp.Value.BaseValue;

        foreach (var kvp in player.BaseStats.Skills)
            data.Skills[kvp.Key] = kvp.Value;

        foreach (var kvp in player.Equipment.EquippedItems)
        {
            var itemData = SerializeItem(kvp.Value);
            itemData.Slot = kvp.Key.ToString();
            data.EquippedItems.Add(itemData);
        }

        foreach (var item in player.Inventory.Items)
            data.InventoryItems.Add(SerializeItem(item));

        string json = JsonSerializer.Serialize(data, _options);
        File.WriteAllText(filePath, json);
        Console.WriteLine($"Game saved to {filePath}");
    }

    public static void LoadGame(Character player, string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Save file not found: {filePath}");
            return;
        }

        string json = File.ReadAllText(filePath);
        var data = JsonSerializer.Deserialize<SaveData>(json, _options);
        if (data == null) return;

        player.Name = data.PlayerName;
        player.Level = data.Level;
        player.Experience = data.Experience;
        player.AvailablePerkPoints = data.AvailablePerkPoints;
        player.AvailableSpecialPoints = data.AvailableSpecialPoints;
        player.Health = data.Health;
        player.Mana = data.Mana;
        player.Stamina = data.Stamina;

        player.BaseStats.Health.BaseValue = data.BaseHealth;
        player.BaseStats.Mana.BaseValue = data.BaseMana;
        player.BaseStats.Stamina.BaseValue = data.BaseStamina;
        player.BaseStats.Defense.BaseValue = data.BaseDefense;
        player.BaseStats.AttackPower.BaseValue = data.BaseAttackPower;
        player.BaseStats.SpellPower.BaseValue = data.BaseSpellPower;
        player.BaseStats.MoveSpeed.BaseValue = data.BaseMoveSpeed;
        player.BaseStats.CritChance.BaseValue = data.BaseCritChance;

        foreach (var kvp in data.PrimaryStats)
        {
            if (Enum.TryParse<PrimaryStat>(kvp.Key, out var stat))
                player.BaseStats.Primary[stat].BaseValue = kvp.Value;
        }

        foreach (var kvp in data.Skills)
        {
            player.BaseStats.Skills[kvp.Key] = kvp.Value;
            if (!player.Proficiencies.Contains(kvp.Key)) player.Proficiencies.Add(kvp.Key);
        }

        foreach (var slot in Enum.GetValues<EquipmentSlot>())
            player.Equipment.Unequip(slot);
        
        foreach (var item in player.Inventory.Items.ToArray())
            player.Inventory.TryRemoveItem(item);

        foreach (var itemData in data.EquippedItems)
        {
            var item = DeserializeItem(itemData);
            if (item != null && Enum.TryParse<EquipmentSlot>(itemData.Slot, out var slot))
                player.Equipment.Equip(slot, item);
        }

        foreach (var itemData in data.InventoryItems)
        {
            var item = DeserializeItem(itemData);
            if (item != null) player.Inventory.TryAddItem(item);
        }

        Console.WriteLine($"Game loaded from {filePath}");
    }

    private static ItemSaveData SerializeItem(Item item)
    {
        var data = new ItemSaveData
        {
            TypeName = item.GetType().Name,
            Name = item.Name,
            Quality = item.Quality,
            Rarity = item.Rarity.ToString(),
            Modifier = item.Modifier.ToString(),
        };

        if (item is Consumable c) data.StackSize = c.StackSize;

        foreach (var slot in item.Composition.Slots)
        {
            data.Parts.Add(new PartSaveData
            {
                PartType = slot.Part.Type.ToString(),
                MaterialType = slot.Material.Type.ToString()
            });
        }

        return data;
    }

    private static Item? DeserializeItem(ItemSaveData data)
    {
        Item? item = data.TypeName switch
        {
            "Dagger" => new Dagger(),
            "Sword" => new Sword(),
            "Axe" => new Axe(),
            "Pike" => new Pike(),
            "Bow" => new Bow(),
            "Crossbow" => new Crossbow(),
            "Shield" => new Game.Core.Items.Types.Weapons.Defense.Shield(),
            "Staff" => new Staff(),
            "Spellbook" => new Spellbook(),
            "HealthPotion" => new HealthPotion(),
            "ManaScroll" => new ManaScroll(),
            "Ring" => new Ring(),
            "Amulet" => new Amulet(),
            "Belt" => new Belt(),
            "Bracers" => new Bracers(),
            _ => null
        };

        if (item == null) return null;

        item.Name = data.Name;
        item.Quality = data.Quality;
        if (Enum.TryParse<Rarity>(data.Rarity, out var r)) item.Rarity = r;
        if (Enum.TryParse<ItemModifier>(data.Modifier, out var m)) item.Modifier = m;

        if (item is Consumable c) c.StackSize = data.StackSize;

        foreach (var partData in data.Parts)
        {
            var material = CreateMaterial(partData.MaterialType);
            if (material == null) continue;
            var part = CreatePart(partData.PartType, material);
            if (part != null) item.Composition.AddPart(part, material);
        }

        item.RebuildPartId();
        return item;
    }

    private static Material? CreateMaterial(string type) => type switch
    {
        "Iron" => new IronMaterial(),
        "Steel" => new SteelMaterial(),
        "Obsidian" => new ObsidianMaterial(),
        _ => null
    };

    private static ItemPart? CreatePart(string type, Material material) => type switch
    {
        "Blade" => new BladePart(material),
        "Guard" => new GuardPart(material),
        "Hilt" => new HiltPart(material),
        "Plate" => new PlatePart(material),
        "Notebook" => new NotebookPart(material),
        _ => null
    };
}