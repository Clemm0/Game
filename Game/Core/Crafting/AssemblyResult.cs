using Game.Core.Items;

namespace Game.Core.Crafting;

public readonly struct AssemblyResult
{
    public Item Item { get; }
    public bool Success { get; }
    public string Message { get; }

    public AssemblyResult(Item item, bool success, string message)
    {
        Item = item;
        Success = success;
        Message = message;
    }
}