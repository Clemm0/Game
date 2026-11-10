using Game.Core.Npcs.Enemies.Base;
using Game.Core.Npcs.Enemies.Elite;
using Game.Core.Npcs.Enemies.Bosses;
using Game.Core.Rpg;

namespace Game.Core.Npcs;

public static class EnemyFactory
{
    // Base 
    public static Character CreateGoblin() => new Goblin();
    public static Character CreateSkeleton() => new Skeleton();
    public static Character CreateGiantCrab() => new GiantCrab();

    // Elites
    public static Character CreateOrc() => new Orc();
    public static Character CreateTroll() => new Troll();

    // Bosses
    public static Character CreateGoblinKing() => new GoblinKing();
}