namespace Game.Core.Items.Types;

public abstract class Weapon : Item
{
    public float BaseDamage { get; protected set; }
    public float AttackSpeed { get; protected set; }
}