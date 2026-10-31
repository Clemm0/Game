using Game.Core.Generic;
using Game.Core.Rpg.Races;

namespace Game.Core.Rpg;

public abstract class Character : GenericEntity
{
    public Race Race { get; protected set; } = null!;
    public CharacterClass Class { get; protected set; } = null!;
    public StatBlock Stats { get; } = new();

    public float Health { get; internal set; }
    public float Mana { get; protected set; }
    public float Stamina { get; protected set; }

    public override void Update(float deltaTime){}
}