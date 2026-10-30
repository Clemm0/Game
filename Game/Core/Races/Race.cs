namespace Game.Core.Rpg.Races;

public abstract class Race
{
    public string Name { get; protected set; } = string.Empty;
    public virtual void Apply(StatBlock stats) { }
}