using System.Collections.Generic;

namespace Game.Core.Rpg;

public abstract class CharacterClass
{
    public string Name { get; protected set; } = string.Empty;
    public List<Skill> Skills { get; } = new();
    public abstract void Apply(StatBlock stats);
}