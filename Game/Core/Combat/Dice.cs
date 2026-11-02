using System;

namespace Game.Core.Combat;

public static class Dice
{
    private static readonly Random _rng = new();

    public static int Roll(int sides)
    {
        return _rng.Next(1, sides + 1);
    }

    public static int Roll(int count, int sides, int modifier = 0)
    {
        int total = 0;
        for (int i = 0; i < count; i++)
        {
            total += Roll(sides);
        }
        return total + modifier;
    }

    public static int RollD20(bool advantage = false, bool disadvantage = false)
    {
        if (advantage && !disadvantage) return Math.Max(Roll(20), Roll(20));
        if (disadvantage && !advantage) return Math.Min(Roll(20), Roll(20));
        return Roll(20);
    }
}