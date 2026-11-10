using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Game.Core.Items.Types;
using Game.Core.Items.Types.Consumables;
using Game.Core.Rpg;
using Game.Core.Rpg.Equipment;

namespace Game.Core.Combat;

public class CombatManager
{
    private readonly List<Character> _players;
    private readonly List<Character> _enemies;
    private readonly bool _autoBattle;
    private readonly ICombatInterface _ui;

    public CombatManager(List<Character> players, List<Character> enemies, bool autoBattle = false, ICombatInterface? ui = null)
    {
        _players = players;
        _enemies = enemies;
        _autoBattle = autoBattle;
        _ui = ui ?? new ConsoleCombatInterface();
    }

    public async Task StartCombatAsync()
    {
        _ui.Log($"\n=== Combat: {string.Join(", ", _players.Select(p => p.Name))} vs {string.Join(", ", _enemies.Select(e => e.Name))} ===");
        if (_autoBattle) _ui.Log("  [Auto-Battle enabled]");

        while (_players.Any(p => p.Health > 0) && _enemies.Any(e => e.Health > 0))
        {
            foreach (var player in _players.Where(p => p.Health > 0).ToList())
            {
                if (!_enemies.Any(e => e.Health > 0)) break;
                if (_autoBattle) AutoPlayerTurn(player);
                else await PlayerTurnAsync(player);
                foreach (var skill in player.Class.Skills) skill.TickTurn();
            }

            foreach (var enemy in _enemies.Where(e => e.Health > 0).ToList())
            {
                if (!_players.Any(p => p.Health > 0)) break;
                EnemyTurn(enemy);
            }

            foreach (var p in _players) p.IsTaunting = false;
        }

        bool playersWin = _players.Any(p => p.Health > 0);
        _ui.Log(playersWin ? "*** The Party Wins! ***" : "*** The Party has fallen... ***");

        if (playersWin)
        {
            int totalXp = _enemies.OfType<Game.Core.Npcs.Monster>().Sum(m => m.ExperienceReward);
            if (totalXp > 0)
            {
                _ui.Log($"\n  Party gained {totalXp} XP each!");
                foreach (var p in _players.Where(p => p.Health > 0))
                    p.GainExperience(totalXp);
            }
        }
    }

    private async Task PlayerTurnAsync(Character player)
    {
        _ui.Log($"\n--- {player.Name}'s Turn (HP: {player.Health:F0}) ---");
        _ui.Log("  Actions:");
        _ui.Log("    [A]ttack");
        if (player.Class.Skills.Any(s => s.IsReady)) _ui.Log("    [S]kill");
        if (player.Inventory.Items.Any(i => i is HealthPotion p && p.StackSize > 0)) _ui.Log("    [P]otion");
        if (player.Class.Skills.Any(s => s.Name == "Taunt" && s.IsReady)) _ui.Log("    [T]aunt");

        string choice = (await _ui.PromptAsync("  > ")).Trim().ToUpper();
        switch (choice)
        {
            case "A": await AttackActionAsync(player); break;
            case "S": await SkillActionAsync(player); break;
            case "P": PotionAction(player); break;
            case "T": TauntAction(player); break;
            default:
                _ui.Log("  Invalid choice.");
                await PlayerTurnAsync(player);
                break;
        }
    }

    private void AutoPlayerTurn(Character player)
    {
        _ui.Log($"\n--- {player.Name}'s Turn (HP: {player.Health:F0}) [Auto] ---");

        var stats = player.GetEffectiveStats();
        if (player.Health / stats.Health.Effective < 0.60f)
        {
            var heal = player.Class.Skills.FirstOrDefault(s => s.Name == "Heal" && s.IsReady);
            if (heal != null)
            {
                var target = _players.Where(p => p.Health > 0).OrderBy(p => p.Health / p.GetEffectiveStats().Health.Effective).First();
                heal.Activate(player, target);
                return;
            }
        }

        var readySkill = player.Class.Skills.FirstOrDefault(s => s.IsReady && s.Name != "Heal");
        if (readySkill != null && _enemies.Any(e => e.Health > 0))
        {
            readySkill.Activate(player, _enemies.First(e => e.Health > 0));
            return;
        }

        var enemy = _enemies.FirstOrDefault(e => e.Health > 0);
        if (enemy != null)
        {
            player.Equipment.EquippedItems.TryGetValue(EquipmentSlot.MainHand, out var equipped);
            var weapon = equipped as Weapon;
            string weaponName = weapon?.Name ?? "Fists";
            float mult = 1f;
            if (weapon != null && !player.Class.PreferredWeapons.Contains(weapon.GetType().Name))
            {
                mult = 0.75f;
                _ui.Log($"  *{player.Name} swings the {weaponName} awkwardly (-25% damage)*");
            }
            BasicAttack(player, enemy, weaponName, mult);
        }
    }

    private async Task AttackActionAsync(Character player)
    {
        var enemies = _enemies.Where(e => e.Health > 0).ToList();
        var target = await _ui.PickTargetAsync(enemies, "Target which enemy?");
        if (target == null) { await PlayerTurnAsync(player); return; }

        player.Equipment.EquippedItems.TryGetValue(EquipmentSlot.MainHand, out var equipped);
        var weapon = equipped as Weapon;
        string weaponName = weapon?.Name ?? "Fists";

        float affinityMult = 1f;
        if (weapon != null && !player.Class.PreferredWeapons.Contains(weapon.GetType().Name))
        {
            affinityMult = 0.75f;
            _ui.Log($"  *{player.Name} swings the {weaponName} awkwardly (-25% damage)*");
        }

        BasicAttack(player, target, weaponName, affinityMult);
    }

    private async Task SkillActionAsync(Character player)
    {
        var readySkills = player.Class.Skills.Where(s => s.IsReady).ToList();
        if (readySkills.Count == 0)
        {
            _ui.Log("  No skills ready.");
            await PlayerTurnAsync(player);
            return;
        }

        var skill = await _ui.PickSkillAsync(readySkills);
        if (skill == null) { await PlayerTurnAsync(player); return; }

        Character? target;
        if (skill.Name == "Heal")
        {
            var wounded = _players.Where(p => p.Health > 0).ToList();
            target = await _ui.PickTargetAsync(wounded, "Heal which ally?");
        }
        else
        {
            target = await _ui.PickTargetAsync(_enemies.Where(e => e.Health > 0).ToList(), "Target which enemy?");
        }

        if (target != null) skill.Activate(player, target);
        else await PlayerTurnAsync(player);
    }

    private void PotionAction(Character player)
    {
        var potion = player.Inventory.Items
            .FirstOrDefault(i => i is HealthPotion p && p.StackSize > 0) as HealthPotion;
        if (potion == null)
        {
            _ui.Log("  No potions left.");
            return;
        }
        potion.Use(player);
        if (potion.StackSize <= 0) player.Inventory.TryRemoveItem(potion);
    }

    private void TauntAction(Character player)
    {
        var taunt = player.Class.Skills.FirstOrDefault(s => s.Name == "Taunt" && s.IsReady);
        if (taunt == null)
        {
            _ui.Log("  No taunt ready.");
            return;
        }
        taunt.Activate(player, _enemies.First(e => e.Health > 0));
    }

    private void EnemyTurn(Character enemy)
    {
        var taunter = _players.FirstOrDefault(p => p.Health > 0 && p.IsTaunting);
        Character? target = taunter ?? _players.Where(p => p.Health > 0).OrderBy(_ => Guid.NewGuid()).FirstOrDefault();

        if (target == null) return;
        _ui.Log($"\n--- {enemy.Name}'s Turn (HP: {enemy.Health:F0}){(taunter != null ? $" [Taunted → {taunter.Name}]" : "")} ---");
        BasicAttack(enemy, target, "Claws", 1f);
    }

    private void BasicAttack(Character attacker, Character defender, string weaponName, float affinityMult)
    {
        var atkStats = attacker.GetEffectiveStats();
        var defStats = defender.GetEffectiveStats();

        int roll = Dice.RollD20();
        int aglMod = (int)atkStats.Primary[PrimaryStat.AGL].Effective;
        int profBonus = 0;
        if (attacker.BaseStats.Skills.TryGetValue(weaponName, out int lvl)) profBonus = lvl;

        int total = roll + attacker.ProficiencyBonus + aglMod + profBonus;
        int targetAC = (int)defStats.Defense.Effective;

        _ui.Log($"  Roll: {roll} + {attacker.ProficiencyBonus}(Prof) + {aglMod}(AGL) + {profBonus}(Skill) = {total} vs AC {targetAC}");

        if (roll == 20 || total >= targetAC)
        {
            int dmg = Dice.Roll(1, 8) + (int)atkStats.Primary[PrimaryStat.STR].Effective;

            int critThreshold = 20 - (int)atkStats.Primary[PrimaryStat.LUC].Effective;
            if (critThreshold < 15) critThreshold = 15;
            if (roll >= critThreshold)
            {
                _ui.Log("  -> CRITICAL HIT!");
                dmg += Dice.Roll(1, 8);
            }

            dmg = Math.Max(1, (int)(dmg * affinityMult));

            defender.Health -= dmg;
            _ui.Log($"  -> Hits {defender.Name} for {dmg} damage! (HP: {Math.Max(0, defender.Health):F0})");
        }
        else
        {
            _ui.Log($"  -> Misses {defender.Name}!");
        }
    }
}