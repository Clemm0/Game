using System.Collections.Generic;
using Game.Core.Rpg;

namespace Game.Core.Dungeons;

public class Room
{
    public int X { get; }
    public int Y { get; }
    public List<Character> Enemies { get; } = new();
    public Chest? Chest { get; set; }
    public bool IsCleared => Enemies.TrueForAll(e => e.Health <= 0);

    public Room(int x, int y)
    {
        X = x;
        Y = y;
    }
}