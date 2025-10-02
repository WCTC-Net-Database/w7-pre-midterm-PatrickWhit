using ConsoleRpgEntities.Models;
using System.Linq;

// =======================
// Assignment - Item-Driven Combat
// =======================
// INSTRUCTIONS:
// This code demonstrates a basic battle loop using AbilityScores for player stats.
// Your assignment is to extend this logic so that equipped items affect attack, defense, and other stats.
// - Equipped weapons should increase attack damage.
// - Equipped armor should reduce incoming damage.
// - Potions or consumables may restore health or provide other effects.
// - Use LINQ to sum up relevant AttributeModifiers from equipped items.
// - For stretch goal: Extend monsters to use items in combat as well (see below).
// - Use player.AbilityScores for all stats (e.g., HitPoints, Attack, Defense, Gold).

public class BattleResult
{
    public List<string> Events { get; set; } = new();
    public bool PlayerWon { get; set; }
}

public class BattleService : IBattleService
{
    public BattleResult Battle(Player player, MonsterBase monster)
    {
        var result = new BattleResult();

        // Use AbilityScores for player's starting HP
        int playerHP = player.AbilityScores.HitPoints;
        int monsterHP = monster.Health;

        result.Events.Add($"Battle started: {player.Name} vs {monster.Name}");

        while (playerHP > 0 && monsterHP > 0)
        {
            // ===========================
            // PLAYER ATTACK CALCULATION
            // ===========================
            // TODO: Extend this logic to use equipped items for attack bonuses.
            int baseAttack = player.AbilityScores.Attack + player.Level;
            int itemAttackBonus = 0; // TODO: Use LINQ to sum equipped items' Attack modifiers
            int totalAttack = baseAttack + itemAttackBonus;

            // TODO: (Stretch) Calculate monster's total defense value using items
            int monsterDefenseBonus = 0; // TODO: Sum monster's equipped items' Defense modifiers

            // Calculate damage to monster (minimum 1)
            int playerDamage = Math.Max(totalAttack - monsterDefenseBonus, 1);

            // Apply damage to monster
            monsterHP -= playerDamage;
            result.Events.Add($"{player.Name} attacks {monster.Name} for {playerDamage} damage! Monster HP: {Math.Max(monsterHP, 0)}");

            if (monsterHP <= 0)
            {
                result.Events.Add($"{monster.Name} is defeated!");
                result.PlayerWon = true;
                break;
            }

            // ===========================
            // MONSTER ATTACK CALCULATION
            // ===========================
            // TODO: (Stretch) Extend this logic to use monster items for attack bonuses.
            int monsterAttack = monster.DealDamage();
            int monsterItemAttackBonus = 0; // TODO: Sum monster's equipped items' Attack modifiers
            int totalMonsterAttack = monsterAttack + monsterItemAttackBonus;

            // PLAYER DEFENSE CALCULATION
            // TODO: Extend this logic to use equipped items for defense bonuses.
            int baseDefense = player.AbilityScores.Defense;
            int itemDefenseBonus = 0; // TODO: Use LINQ to sum equipped items' Defense modifiers
            int totalDefense = baseDefense + itemDefenseBonus;

            // Calculate damage to player (minimum 1)
            int monsterDamage = Math.Max(totalMonsterAttack - totalDefense, 1);

            // Apply damage to player
            playerHP -= monsterDamage;
            result.Events.Add($"{monster.Name} attacks {player.Name} for {monsterDamage} damage! Player HP: {Math.Max(playerHP, 0)}");

            if (playerHP <= 0)
            {
                result.Events.Add($"{player.Name} is defeated!");
                result.PlayerWon = false;
                break;
            }

            // ===========================
            // (Optional) Consumable Logic
            // ===========================
            // TODO: Implement logic for consumable items (e.g., potions) to restore health or provide effects.
            // You may prompt the player to use a potion or automatically use it when health is low.
        }
        return result;
    }
}
