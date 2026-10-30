using System.Collections.Generic;
using Game.Core.Items.Materials;
using Game.Core.Rpg;

namespace Game.Core.Items.Types.Armors;

/// <summary>
/// A named collection of ArmorPieces. Each set declares its slots,
/// and the constructor composes every piece with the material.
/// </summary>
public abstract class ArmorSet
{
    public string Name { get; protected set; } = string.Empty;
    public Material PrimaryMaterial { get; protected set; } = null!;

    public HashSet<ArmorSlot> Slots { get; } = new();
    public Dictionary<ArmorSlot, ArmorPiece> Pieces { get; } = new();

    /// <summary>Bonus stats granted when every declared slot is filled.</summary>
    public virtual StatBlock? SetBonus() => null;

    /// <summary>
    /// Helper used by every concrete set: register a piece and compose it
    /// with `plateCount` plates of `PrimaryMaterial`.
    /// </summary>
    protected T AddPiece<T>(T piece, int plateCount) where T : ArmorPiece
    {
        piece.BuildWith(PrimaryMaterial, plateCount);
        Pieces[piece.Slot] = piece;
        Slots.Add(piece.Slot);
        return piece;
    }

    /// <summary>Equip check — fills exactly the declared slots.</summary>
    public bool IsComplete(IReadOnlyDictionary<ArmorSlot, ArmorPiece> equipped)
    {
        foreach (var slot in Slots)
            if (!equipped.ContainsKey(slot)) return false;
        return true;
    }
}