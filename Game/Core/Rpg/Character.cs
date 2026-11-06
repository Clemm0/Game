using System;
using System.Collections.Generic;
using Game.Core.Generic;
using Game.Core.Items.Types.Armors;
using Game.Core.Rpg.Equipment;
using Game.Core.Rpg.Races;

namespace Game.Core.Rpg;

public abstract class Character : GenericEntity
{
    public Race Race { get; protected set; } = null!;
    public CharacterClass Class { get; protected set; } = null!;
    public StatBlock BaseStats { get; } = new();
    public EquipmentManager Equipment { get; } = new();
    public Game.Core.Inventory.Inventory Inventory { get; } = new();

    public int Level { get; set; } = 1;
    public int Experience { get; set; } = 0;
    public int ExperienceToNextLevel => Level * 100;
    
    public int AvailableSpecialPoints { get; set; } = 0;
    public int AvailablePerkPoints { get; set; } = 0;

    public HashSet<string> Proficiencies { get; } = new();

    public int ProficiencyBonus => 2 + ((Level - 1) / 4);

    public float Health { get; set; }
    public float Mana { get; set; }
    public float Stamina { get; set; }

    public override void Update(float deltaTime)
    {
        var stats = GetEffectiveStats();
        
        float hpRegen = 1f + (stats.Health.Effective * 0.01f); 
        float mpRegen = 2f + (stats.Mana.Effective * 0.02f);
        float spRegen = 5f + (stats.Stamina.Effective * 0.05f);

        Health = Math.Min(Health + (hpRegen * deltaTime), stats.Health.Effective);
        Mana = Math.Min(Mana + (mpRegen * deltaTime), stats.Mana.Effective);
        Stamina = Math.Min(Stamina + (spRegen * deltaTime), stats.Stamina.Effective);
    }

    public int GainExperience(int amount)
    {
        Experience += amount;
        int levelsGained = 0;
        
        while (Experience >= ExperienceToNextLevel)
        {
            Experience -= ExperienceToNextLevel;
            Level++;
            levelsGained++;
            
            AvailablePerkPoints += 1;
            
            if (Level % 5 == 0)
            {
                AvailableSpecialPoints += 1;
                Console.WriteLine($"*** {Name} reached Level {Level}! (Gained 1 Perk Point, 1 SPECIAL Point) ***");
            }
            else
            {
                Console.WriteLine($"*** {Name} reached Level {Level}! (Gained 1 Perk Point) ***");
            }
            
            Health = GetEffectiveStats().Health.Effective;
        }
        
        return levelsGained;
    }

    public bool AllocateSpecialPoint(PrimaryStat stat)
    {
        if (AvailableSpecialPoints <= 0) return false;
        BaseStats.Primary[stat].BaseValue += 1f;
        AvailableSpecialPoints--;
        Console.WriteLine($"  -> {stat} increased to {BaseStats.Primary[stat].Effective}!");
        return true;
    }

    public bool AllocatePerkPoint(string proficiency)
    {
        if (AvailablePerkPoints <= 0) return false;
        if (!Proficiencies.Contains(proficiency)) Proficiencies.Add(proficiency);
        
        BaseStats.Skills.TryGetValue(proficiency, out int current);
        BaseStats.Skills[proficiency] = current + 1;
        
        AvailablePerkPoints--;
        Console.WriteLine($"  -> {proficiency} proficiency increased to {BaseStats.Skills[proficiency]}!");
        return true;
    }

    public StatBlock GetEffectiveStats()
    {
        var effective = new StatBlock();
        
        foreach (var kvp in BaseStats.Primary) effective.Primary[kvp.Key].BaseValue += kvp.Value.Effective;
        effective.Health.BaseValue += BaseStats.Health.Effective;
        effective.Stamina.BaseValue += BaseStats.Stamina.Effective;
        effective.Mana.BaseValue += BaseStats.Mana.Effective;
        effective.Defense.BaseValue += BaseStats.Defense.Effective;
        effective.AttackPower.BaseValue += BaseStats.AttackPower.Effective;
        effective.SpellPower.BaseValue += BaseStats.SpellPower.Effective;
        effective.MoveSpeed.BaseValue += BaseStats.MoveSpeed.Effective;
        effective.CritChance.BaseValue += BaseStats.CritChance.Effective;

        foreach (var item in Equipment.EquippedItems.Values)
        {
            if (item == null) continue;
            var itemStats = item.ResolveStats();
            
            effective.Health.BaseValue += itemStats.Health.Effective;
            effective.Defense.BaseValue += itemStats.Defense.Effective;
            effective.AttackPower.BaseValue += itemStats.AttackPower.Effective;
            effective.SpellPower.BaseValue += itemStats.SpellPower.Effective;
            effective.MoveSpeed.PercentBonus += itemStats.MoveSpeed.PercentBonus;
            effective.CritChance.BaseValue += itemStats.CritChance.Effective;
            
            foreach (var kvp in itemStats.Primary) 
                effective.Primary[kvp.Key].BaseValue += kvp.Value.Effective;
        }

        var setPieces = new Dictionary<string, int>();
        foreach (var item in Equipment.EquippedItems.Values)
        {
            if (item is ArmorPiece piece)
            {
                if (!setPieces.ContainsKey(piece.SetName)) setPieces[piece.SetName] = 0;
                setPieces[piece.SetName]++;
            }
        }

        foreach (var kvp in setPieces)
        {
            var setData = ArmorSetRegistry.GetSet(kvp.Key);
            if (setData.HasValue && kvp.Value >= setData.Value.RequiredPieces)
            {
                var bonus = setData.Value.BonusFactory();
                effective.Health.BaseValue += bonus.Health.Effective;
                effective.Defense.PercentBonus += bonus.Defense.PercentBonus;
                effective.MoveSpeed.PercentBonus += bonus.MoveSpeed.PercentBonus;
                effective.AttackPower.BaseValue += bonus.AttackPower.Effective;
            }
        }

        return effective;
    }
}