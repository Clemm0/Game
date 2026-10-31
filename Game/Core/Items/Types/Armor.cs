namespace Game.Core.Items.Types;

public abstract class Armor : Item
{
    public float BaseDefense { get; internal set; }
    public new float Weight { get; internal set; }
}