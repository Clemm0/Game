using Game.Core.Rpg;

namespace Game.Core.Biomes;

public class IcyPeaksBiome : Biome
{
    public IcyPeaksBiome() { Name = "Icy Peaks"; }
    
    public override void TickEnvironment(Character c, float dt)
    {
        c.Health -= 2f * dt; 
    }
}