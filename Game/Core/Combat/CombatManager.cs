using System;
using System.Linq;
using Game.Core.Rpg;
using Game.Core.Rpg.Equipment;
using Game.Core.Items.Types;

namespace Game.Core.Combat;

public class CombatManager
{
    private readonly Character _player;
    private readonly Character _enemy;
    
    public CombatManager(Character player, Character enemy)
    {
        _player = player;
        _enemy = enemy;
    }

    public void StartCombat()
    {
        Console.WriteLine($"\n=== Combat Started: {_player.Name} vs {_enemy.Name} ===");
        
        while (_player.Health > 0 && _enemy.Health > 0)
        {
            foreach (var skill in _player.Class.Skills) skill.Tick(2.0f);
            foreach (var skill in _enemy.Class.Skills) skill.Tick(2.0f);

            PlayerTurn();
            if (_enemy.Health <= 0) break;
            
            EnemyTurn();
        }

        Console.WriteLine(_player.Health > 0 ? $"*** {_player.Name} Wins! ***" : $"*** {_enemy.Name} Wins! ***");
    }

    private void PlayerTurn()
    {
        Console.WriteLine($"\n{_player.Name}'s Turn (HP: {_player.Health:F1})");

        var stats = _player.GetEffectiveStats();
        if (_player.Health < stats.Health.Effective * 0.5f)
        {
            var potion = _player.Inventory.Items
                .FirstOrDefault(i => i is Game.Core.Items.Types.Consumables.HealthPotion p && p.StackSize > 0) 
                as Game.Core.Items.Types.Consumables.HealthPotion;
            
            if (potion != null)
            {
                potion.Use(_player);
                if (potion.StackSize <= 0) _player.Inventory.TryRemoveItem(potion);
                return;
            }
        }

        var usableSkill = _player.Class.Skills.FirstOrDefault(s => s.IsReady);
        if (usableSkill != null)
        {
            usableSkill.Activate(_player, _enemy);
            return;
        }

        Console.WriteLine("  *No skills ready, basic attack!*");
        _player.Equipment.EquippedItems.TryGetValue(EquipmentSlot.MainHand, out var equippedItem);
        var weapon = equippedItem as Weapon;
        Attack(_player, _enemy, 1, 8, weapon?.Name ?? "Fists");
    }

    private void EnemyTurn()
    {
        Console.WriteLine($"{_enemy.Name}'s Turn (HP: {_enemy.Health:F1})");
        Attack(_enemy, _player, 1, 6, "Claws"); 
    }

    private void Attack(Character attacker, Character defender, int diceCount, int diceSides, string attackName)
    {
        var attackerStats = attacker.GetEffectiveStats();
        var defenderStats = defender.GetEffectiveStats();

        int attackRoll = Dice.RollD20();
        int aglMod = (int)attackerStats.Primary[PrimaryStat.AGL].Effective;
        
        int proficiencyBonus = 0;
        if (attacker.Proficiencies.Contains(attackName) && attacker.BaseStats.Skills.TryGetValue(attackName, out int skillLevel))
        {
            proficiencyBonus = skillLevel;
        }

        int totalAttack = attackRoll + attacker.ProficiencyBonus + aglMod + proficiencyBonus;
        int targetAC = (int)defenderStats.Defense.Effective;

        Console.WriteLine($"  -> Roll: {attackRoll} + {attacker.ProficiencyBonus}(Prof) + {aglMod}(AGL) + {proficiencyBonus}(Skill) = {totalAttack} vs AC {targetAC}");

        if (attackRoll == 20 || totalAttack >= targetAC)
        {
            int damage = Dice.Roll(diceCount, diceSides);
            damage += (int)attackerStats.Primary[PrimaryStat.STR].Effective;
            
            int critThreshold = 20 - (int)attackerStats.Primary[PrimaryStat.LUC].Effective;
            if (critThreshold < 15) critThreshold = 15;

            if (attackRoll >= critThreshold)
            {
                Console.WriteLine($"  -> CRITICAL HIT! (Rolled {attackRoll}, needed {critThreshold} due to Luck)");
                damage += Dice.Roll(diceCount, diceSides);
            }

            defender.Health -= damage;
            Console.WriteLine($"  -> Hits {defender.Name} for {damage} damage! (Remaining HP: {Math.Max(0, defender.Health)})");
        }
        else
        {
            Console.WriteLine($"  -> Misses {defender.Name}!");
        }
    }
}