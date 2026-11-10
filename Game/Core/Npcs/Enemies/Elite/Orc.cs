using System;
using System.Collections.Generic;
using Game.Core.Crafting;
using Game.Core.Items;
using Game.Core.Items.Materials;
using Game.Core.Items.Parts;
using Game.Core.Items.Types.Weapons.Melee;
using Game.Core.Rpg;

namespace Game.Core.Npcs.Enemies.Elite;

public class Orc : Monster
{
    public Orc()
    {
        Name = "Orc";
        ExperienceReward = 70;
        BaseStats.Health.BaseValue = 30f;
        Health = 30f;
        BaseStats.Defense.BaseValue = 13f;
        BaseStats.Primary[PrimaryStat.STR].BaseValue = 4f;
        BaseStats.Primary[PrimaryStat.AGL].BaseValue = 2f;

        LootTable = () =>
        {
            var items = new List<Item>();
            if (Random.Shared.Next(100) < 70)
            {
                var axe = new Axe();
                ItemAssembler.Assemble(axe, new List<ItemPart> { new BladePart(new IronMaterial()) }, 0.85f);
                items.Add(axe);
            }
            return items;
        };
    }
}