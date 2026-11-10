using System.Collections.Generic;
using Godot;
using Game.Core.Rpg;
using Game.UI.Characters;

namespace Game.UI;

public partial class GameState : Node
{
    public static GameState Instance { get; private set; } = null!;

    public List<Character> Party { get; private set; } = new();
    public List<Character> CurrentEnemies { get; set; } = new();
    public int CurrentRoomIndex { get; set; } = 0;
    public int TotalRooms { get; set; } = 5;

    public override void _Ready()
    {
        Instance = this;
        GD.Print("GameState initialized");
    }

    public void StartNewGame()
    {
        Party.Clear();
        CurrentRoomIndex = 0;

        Party.Add(new WarriorCharacter { Name = "Brutus" });
        Party.Add(new MageCharacter { Name = "Merlin" });
        Party.Add(new ClericCharacter { Name = "Sister Faith" });

        SpawnEnemiesForCurrentRoom();
        GetTree().ChangeSceneToFile("res://scenes/CombatScene.tscn");
    }

    public void SpawnEnemiesForCurrentRoom()
    {
        CurrentEnemies.Clear();
        var rng = new RandomNumberGenerator();
        rng.Randomize();

        int roll = (int)(rng.Randi() % 100);
        if (roll < 15) CurrentEnemies.Add(Game.Core.Npcs.EnemyFactory.CreateSkeleton());
        else if (roll < 30) CurrentEnemies.Add(Game.Core.Npcs.EnemyFactory.CreateOrc());
        else CurrentEnemies.Add(Game.Core.Npcs.EnemyFactory.CreateGoblin());

        // Level 1 has a boss on the final room
        if (CurrentRoomIndex == TotalRooms - 1)
        {
            CurrentEnemies.Clear();
            CurrentEnemies.Add(Game.Core.Npcs.EnemyFactory.CreateGoblinKing());
        }
    }

    public void AdvanceRoom()
    {
        CurrentRoomIndex++;

        if (CurrentRoomIndex >= TotalRooms)
        {
            GD.Print("Dungeon cleared!");
            // GetTree().ChangeSceneToFile("res://scenes/VictoryScreen.tscn");
            return;
        }

        SpawnEnemiesForCurrentRoom();
        GetTree().ChangeSceneToFile("res://scenes/CombatScene.tscn");
    }
}