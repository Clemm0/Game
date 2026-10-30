using Game.Core.Rpg;

namespace Game.Core.Biomes;

public abstract class Biome
{
    public string Name { get; protected set; } = string.Empty;
    public abstract void TickEnvironment(Character c, float dt);
}