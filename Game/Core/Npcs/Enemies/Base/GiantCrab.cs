using System;
using System.Collections.Generic;
using Game.Core.Crafting;
using Game.Core.Items;
using Game.Core.Items.Materials;
using Game.Core.Items.Parts;
using Game.Core.Items.Types.Accessories;
using Game.Core.Rpg;

namespace Game.Core.Npcs.Enemies.Base;

public class GiantCrab : Monster
{
    public GiantCrab()
    {
        Name = "Giant Crab";
        ExperienceReward = 100;
        BaseStats.Health.BaseValue = 35f;
        Health = 35f;
        BaseStats.Defense.BaseValue = 15f;
        BaseStats.Primary[PrimaryStat.STR].BaseValue = 3f;
        BaseStats.Primary[PrimaryStat.AGL].BaseValue = 1f;

        LootTable = () =>
        {
            var items = new List<Item>();
            if (Random.Shared.Next(100) < 90)
            {
                var ring = new Ring();
                ItemAssembler.Assemble(ring, new List<ItemPart> { new PlatePart(new ObsidianMaterial()) }, 1.0f);
                ring.Name = "Crab's Ring";
                items.Add(ring);
            }
            return items;
        };
    }
}