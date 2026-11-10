using System;
using System.Collections.Generic;
using Game.Core.Crafting;
using Game.Core.Items;
using Game.Core.Items.Materials;
using Game.Core.Items.Parts;
using Game.Core.Items.Types.Weapons.Melee;
using Game.Core.Rpg;

namespace Game.Core.Npcs.Enemies.Elite;

public class Troll : Monster
{
    public Troll()
    {
        Name = "Troll";
        ExperienceReward = 150;
        BaseStats.Health.BaseValue = 60f;
        Health = 60f;
        BaseStats.Defense.BaseValue = 14f;
        BaseStats.Primary[PrimaryStat.STR].BaseValue = 6f;
        BaseStats.Primary[PrimaryStat.AGL].BaseValue = 1f;

        LootTable = () =>
        {
            var items = new List<Item>();
            if (Random.Shared.Next(100) < 80)
            {
                var sword = new Sword();
                ItemAssembler.Assemble(sword, new List<ItemPart> { new BladePart(new ObsidianMaterial()) }, 1.0f);
                items.Add(sword);
            }
            return items;
        };
    }
}