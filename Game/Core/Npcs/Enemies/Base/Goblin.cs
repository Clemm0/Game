using System;
using System.Collections.Generic;
using Game.Core.Crafting;
using Game.Core.Items;
using Game.Core.Items.Materials;
using Game.Core.Items.Parts;
using Game.Core.Items.Types.Weapons.Melee;
using Game.Core.Rpg;

namespace Game.Core.Npcs.Enemies.Base;

public class Goblin : Monster
{
    public Goblin()
    {
        Name = "Goblin";
        ExperienceReward = 25;
        BaseStats.Health.BaseValue = 15f;
        Health = 15f;
        BaseStats.Defense.BaseValue = 10f;
        BaseStats.Primary[PrimaryStat.STR].BaseValue = 1f;
        BaseStats.Primary[PrimaryStat.AGL].BaseValue = 2f;

        LootTable = () =>
        {
            var items = new List<Item>();
            if (Random.Shared.Next(100) < 60)
            {
                var dagger = new Dagger();
                ItemAssembler.Assemble(dagger, new List<ItemPart> { new BladePart(new IronMaterial()) }, 0.8f);
                items.Add(dagger);
            }
            return items;
        };
    }
}