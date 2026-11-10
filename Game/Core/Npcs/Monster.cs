using System;
using System.Collections.Generic;
using Game.Core.Items;
using Game.Core.Rpg;
using Game.Core.Rpg.Races;

namespace Game.Core.Npcs;

public class Monster : Character
{
    public Func<List<Item>>? LootTable { get; set; }
    public int ExperienceReward { get; set; } = 25;

    public Monster()
    {
        Race = new CommonerRace();
        Class = new MonsterClass();
    }
}

public class MonsterClass : CharacterClass
{
    public MonsterClass() { Name = "Monster"; }
    public override void Apply(StatBlock stats) { }
}