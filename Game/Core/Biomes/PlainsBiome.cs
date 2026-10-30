using Game.Core.Biomes;
using Game.Core.Rpg;

namespace Game.Core.Biomes;

public class PlainsBiome : Biome
{
    public PlainsBiome() { Name = "Plains"; }
    public override void TickEnvironment(Character c, float dt) { }
}