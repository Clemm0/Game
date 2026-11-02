using System;
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
            PlayerTurn();
            if (_enemy.Health <= 0) break;
            
            EnemyTurn();
        }

        Console.WriteLine(_player.Health > 0 ? $"*** {_player.Name} Wins! ***" : $"*** {_enemy.Name} Wins! ***");
    }

    private void PlayerTurn()
    {
        Console.WriteLine($"\n{_player.Name}'s Turn (HP: {_player.Health:F1})");
        
        _player.Equipment.EquippedItems.TryGetValue(EquipmentSlot.MainHand, out var equippedItem);
        var weapon = equippedItem as Weapon;
        
        if (weapon == null)
        {
            Console.WriteLine("  *Punches wildly*");
            Attack(_player, _enemy, 1, 3, "Fists"); 
        }
        else
        {
            Console.WriteLine($"  *Attacks with {weapon.Name}*");
            Attack(_player, _enemy, 1, 8, weapon.Name);
        }
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
        int totalAttack = attackRoll + attacker.ProficiencyBonus + aglMod;

        int targetAC = (int)defenderStats.Defense.Effective;

        Console.WriteLine($"  -> Roll: {attackRoll} + {attacker.ProficiencyBonus}(Prof) + {aglMod}(AGL) = {totalAttack} vs AC {targetAC}");

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