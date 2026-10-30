namespace Game.Core.Items.Types;

public abstract class Armor : Item
{
    public float BaseDefense { get; internal set; }
    public float Weight { get; internal set; }
}