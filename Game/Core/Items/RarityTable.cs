using System.Collections.Generic;

namespace Game.Core.Items;

public static class RarityTable
{
    public static readonly Dictionary<Rarity, RarityProfile> Data = new()
    {
        [Rarity.Common]    = new(0.55f, 0.90f, 0.10f, 0.70f),
        [Rarity.Uncommon]  = new(0.25f, 1.00f, 0.20f, 0.70f),
        [Rarity.Rare]      = new(0.12f, 1.10f, 0.30f, 0.80f),
        [Rarity.Epic]      = new(0.05f, 1.25f, 0.35f, 0.90f),
        [Rarity.Legendary] = new(0.02f, 1.50f, 0.50f, 0.99f),
        [Rarity.Elite]     = new(0.01f, 1.75f, 0.60f, 1.00f),
    };

    public readonly record struct RarityProfile(
        float DropChance, float MainStatMultiplier, float QualityMin, float QualityMax);
}