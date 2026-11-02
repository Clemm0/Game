using System;
using System.Collections.Generic;
using Game.Core.Rpg;

namespace Game.Core.Items.Types.Armors;

public static class ArmorSetRegistry
{
    private static readonly Dictionary<string, (int RequiredPieces, Func<StatBlock> BonusFactory)> Sets = new();

    public static void Register(string setName, int requiredPieces, Func<StatBlock> bonusFactory)
    {
        Sets[setName] = (requiredPieces, bonusFactory);
    }

    public static (int RequiredPieces, Func<StatBlock> BonusFactory)? GetSet(string setName)
    {
        if (Sets.TryGetValue(setName, out var data)) return data;
        return null;
    }
}