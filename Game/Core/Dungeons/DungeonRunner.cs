using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Game.Core.Combat;
using Game.Core.Crafting;
using Game.Core.Items;
using Game.Core.Items.Materials;
using Game.Core.Items.Parts;
using Game.Core.Items.Types.Consumables;
using Game.Core.Items.Types.Weapons.Melee;
using Game.Core.Npcs;
using Game.Core.Rpg;

namespace Game.Core.Dungeons;

public class DungeonRunner
{
    private readonly List<Character> _players;
    private readonly List<Room> _rooms;
    private readonly Random _rng = new();
    private readonly bool _autoBattle;

    public DungeonRunner(List<Character> players, string dungeonName, int roomCount, bool autoBattle = false)
    {
        _players = players;
        _autoBattle = autoBattle;
        var generator = new DungeonGenerator();
        _rooms = generator.Generate(roomCount);

        for (int r = 0; r < _rooms.Count; r++)
        {
            var room = _rooms[r];
            bool isBossFloor = (r == _rooms.Count - 1) && (_rooms.Count >= 5 || _rng.Next(100) < 20);

            if (isBossFloor)
            {
                room.Enemies.Clear();
                room.Enemies.Add(EnemyFactory.CreateGoblinKing());
                Console.WriteLine("  [BOSS FLOOR] The Goblin King awaits!");
            }
            else
            {
                int enemyCount = _rng.Next(1, 4);
                for (int i = 0; i < enemyCount; i++)
                {
                    int roll = _rng.Next(100);
                    if (roll < 5) room.Enemies.Add(EnemyFactory.CreateTroll());
                    else if (roll < 15) room.Enemies.Add(EnemyFactory.CreateGiantCrab());
                    else if (roll < 35) room.Enemies.Add(EnemyFactory.CreateOrc());
                    else if (roll < 60) room.Enemies.Add(EnemyFactory.CreateSkeleton());
                    else room.Enemies.Add(EnemyFactory.CreateGoblin());
                }
            }

            int chestRoll = _rng.Next(100);
            if (chestRoll < 40)
            {
                room.Chest = new Chest
                {
                    Name = "Wooden Chest",
                    IsLocked = _rng.Next(100) < 50,
                    LockDifficulty = 10 + _rng.Next(5)
                };
                int lootCount = _rng.Next(1, 3);
                for (int i = 0; i < lootCount; i++)
                {
                    var sword = new Sword();
                    ItemAssembler.Assemble(sword, new List<ItemPart> { new BladePart(new SteelMaterial()) }, 1.0f);
                    sword.Name = "Chest Sword";
                    room.Chest.Contents.Add(sword);
                }
                if (_rng.Next(100) < 50)
                    room.Chest.Contents.Add(new HealthPotion(20f) { StackSize = 2 });
            }
        }
    }

    public async Task RunPartyDungeonAsync()
    {
        Console.WriteLine($"\n=== Entering the Dungeon ({_rooms.Count} Rooms){( _autoBattle ? " [Auto]" : "")} ===");

        for (int i = 0; i < _rooms.Count; i++)
        {
            var room = _rooms[i];
            Console.WriteLine($"\n--- Room {i + 1} (Coords: {room.X}, {room.Y}) ---");

            if (room.Enemies.Count > 0)
            {
                Console.WriteLine($"You encounter {room.Enemies.Count} enemies!");
                var combat = new CombatManager(_players, room.Enemies, _autoBattle);
                await combat.StartCombatAsync();

                var survivors = _players.Where(p => p.Health > 0).ToList();
                if (survivors.Count == 0)
                {
                    Console.WriteLine("\nThe whole party has fallen...");
                    return;
                }
                _players.Clear();
                _players.AddRange(survivors);
                Console.WriteLine("Room cleared!");

                foreach (var enemy in room.Enemies)
                {
                    if (enemy is Monster monster && monster.LootTable != null)
                    {
                        var loot = monster.LootTable();
                        if (loot.Count > 0)
                        {
                            Console.WriteLine($"  {enemy.Name} dropped:");
                            foreach (var item in loot)
                            {
                                Console.WriteLine($"    - {item.Name}");
                                foreach (var p in _players)
                                {
                                    if (p.Inventory.TryAddItem(item))
                                    {
                                        Console.WriteLine($"      → {p.Name} picked it up.");
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (room.Chest != null && !room.Chest.IsOpened)
            {
                Console.WriteLine($"  You find a {room.Chest.Name}!");
                foreach (var p in _players)
                {
                    if (p.Health > 0)
                    {
                        room.Chest.Open(p);
                        break;
                    }
                }
            }
        }

        Console.WriteLine("\n=== Dungeon Cleared! ===");
    }
}