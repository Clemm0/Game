using Game.Core.Combat;

namespace Game.Core.Rpg;

public abstract class Skill
{
    public string Name { get; protected set; } = string.Empty;
    public string Description { get; protected set; } = string.Empty;
    public float Cooldown { get; protected set; }
    public float CurrentCooldown { get; protected set; }

    public bool IsReady => CurrentCooldown <= 0f;

    public abstract void Activate(Character attacker, Character target);

    public virtual void Tick(float deltaTime)
    {
        if (CurrentCooldown > 0) CurrentCooldown -= deltaTime;
    }
}