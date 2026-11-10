using System;
using System.Collections.Generic;

namespace Game.Core.Items;

public static class LootTable
{
    private static readonly Random _rng = new();

    public static Rarity RollRarity(float luckModifier = 0f)
    {
        double roll = _rng.NextDouble();
        
        roll -= luckModifier; 

        float currentThreshold = 0f;

        currentThreshold += 0.55f;
        if (roll < currentThreshold) return Rarity.Common;

        currentThreshold += 0.25f;
        if (roll < currentThreshold) return Rarity.Uncommon;

        currentThreshold += 0.12f;
        if (roll < currentThreshold) return Rarity.Rare;

        currentThreshold += 0.05f;
        if (roll < currentThreshold) return Rarity.Epic;

        currentThreshold += 0.02f;
        if (roll < currentThreshold) return Rarity.Legendary;

        return Rarity.Elite;
    }
}