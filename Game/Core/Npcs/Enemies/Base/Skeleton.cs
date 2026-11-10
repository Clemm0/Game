using System;
using System.Collections.Generic;
using Game.Core.Crafting;
using Game.Core.Items;
using Game.Core.Items.Materials;
using Game.Core.Items.Parts;
using Game.Core.Items.Types.Weapons.Melee;
using Game.Core.Rpg;

namespace Game.Core.Npcs.Enemies.Base;

public class Skeleton : Monster
{
    public Skeleton()
    {
        Name = "Skeleton";
        ExperienceReward = 40;
        BaseStats.Health.BaseValue = 20f;
        Health = 20f;
        BaseStats.Defense.BaseValue = 12f;
        BaseStats.Primary[PrimaryStat.STR].BaseValue = 2f;
        BaseStats.Primary[PrimaryStat.AGL].BaseValue = 1f;

        LootTable = () =>
        {
            var items = new List<Item>();
            if (Random.Shared.Next(100) < 70)
            {
                var sword = new Sword();
                ItemAssembler.Assemble(sword, new List<ItemPart> { new BladePart(new SteelMaterial()), new GuardPart(new IronMaterial()) }, 0.7f);
                items.Add(sword);
            }
            return items;
        };
    }
}