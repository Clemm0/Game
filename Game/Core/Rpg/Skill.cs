namespace Game.Core.Rpg;

public abstract class Skill
{
    public string Name { get; protected set; } = string.Empty;
    public float Duration { get; protected set; }
    public float Cooldown { get; protected set; }

    public virtual void Activate(Character owner, Character? target) { }
}