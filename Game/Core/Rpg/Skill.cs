namespace Game.Core.Rpg;

public abstract class Skill
{
    public string Name { get; protected set; } = string.Empty;
    public string Description { get; protected set; } = string.Empty;
    
    public int CooldownTurns { get; protected set; }
    
    public int TurnsUntilReady { get; protected set; }

    public bool IsReady => TurnsUntilReady <= 0;

    public abstract void Activate(Character attacker, Character target);

    public void TickTurn()
    {
        if (TurnsUntilReady > 0) TurnsUntilReady--;
    }

    protected void PutOnCooldown() => TurnsUntilReady = CooldownTurns;
}