namespace Game.Core.Items.Types;

public abstract class Accessory : Item
{
    public float StatMultiplier { get; protected set; } = 1f;
}