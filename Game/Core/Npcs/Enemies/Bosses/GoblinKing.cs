using System;
using System.Collections.Generic;
using Game.Core.Crafting;
using Game.Core.Items;
using Game.Core.Items.Materials;
using Game.Core.Items.Parts;
using Game.Core.Items.Types.Weapons.Melee;
using Game.Core.Rpg;

namespace Game.Core.Npcs.Enemies.Bosses;

public class GoblinKing : Monster
{
    public GoblinKing()
    {
        Name = "Goblin King";
        ExperienceReward = 300;
        BaseStats.Health.BaseValue = 80f;
        Health = 80f;
        BaseStats.Defense.BaseValue = 16f;
        BaseStats.Primary[PrimaryStat.STR].BaseValue = 5f;
        BaseStats.Primary[PrimaryStat.AGL].BaseValue = 3f;

        LootTable = () =>
        {
            var items = new List<Item>();
            var sword = new Sword();
            ItemAssembler.Assemble(sword, new List<ItemPart> { new BladePart(new ObsidianMaterial()), new GuardPart(new SteelMaterial()) }, 1.0f);
            sword.Name = "Goblin King's Blade";
            sword.Rarity = Rarity.Legendary;
            items.Add(sword);
            return items;
        };
    }
}