using Game.Core.Rpg;

namespace Game.Core.Items.Types;

public abstract class Consumable : Item
{
    public int StackSize { get; set; } = 1;
    public int MaxStackSize { get; protected set; } = 10;
    public virtual bool IsConsumedOnUse => true;

    public virtual void Use(Character target)
    {
        ApplyEffect(target);
        if (IsConsumedOnUse) StackSize--;
    }

    public abstract void ApplyEffect(Character target);
}