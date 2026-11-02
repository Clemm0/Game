using System.Collections.Generic;
using Game.Core.Items.Materials;
using Game.Core.Rpg;

namespace Game.Core.Items.Types.Armors;

public abstract class ArmorSet
{
    public string Name { get; protected set; } = string.Empty;
    public Material PrimaryMaterial { get; protected set; } = null!;

    public HashSet<ArmorSlot> Slots { get; } = new();
    public Dictionary<ArmorSlot, ArmorPiece> Pieces { get; } = new();

    public virtual StatBlock? SetBonus() => null;

    protected T AddPiece<T>(T piece, int plateCount) where T : ArmorPiece
    {
        piece.BuildWith(PrimaryMaterial, plateCount);
        piece.Quality = 1.0f;
        piece.Modifier = ItemModifier.Pristine;
        Pieces[piece.Slot] = piece;
        Slots.Add(piece.Slot);
        return piece;
    }

    public bool IsComplete(IReadOnlyDictionary<ArmorSlot, ArmorPiece> equipped)
    {
        foreach (var slot in Slots)
            if (!equipped.ContainsKey(slot)) return false;
        return true;
    }
}