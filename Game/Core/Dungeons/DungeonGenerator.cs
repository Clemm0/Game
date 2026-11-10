using System;
using System.Collections.Generic;

namespace Game.Core.Dungeons;

public class DungeonGenerator
{
    private readonly Random _rng = new();
    private readonly HashSet<(int x, int y)> _occupied = new();

    public List<Room> Generate(int floorCount, int maxSpiralRadius = 3)
    {
        var rooms = new List<Room>();
        int x = 0, y = 0;
        int dx = 1, dy = 0;
        int stepLength = 1;
        int stepCounter = 0;

        for (int i = 0; i < floorCount; i++)
        {
            if (!_occupied.Contains((x, y)))
            {
                var room = new Room(x, y);
                _occupied.Add((x, y));
                rooms.Add(room);
            }

            x += dx;
            y += dy;
            stepCounter++;

            if (stepCounter == stepLength)
            {
                stepCounter = 0;
                
                int temp = dx;
                dx = -dy;
                dy = temp;

                if (dy == 0) stepLength++;
            }

            if (Math.Abs(x) > maxSpiralRadius || Math.Abs(y) > maxSpiralRadius) break;
        }

        return rooms;
    }
}