using System.Collections.Generic;
using System.Linq;

namespace ConsoleRpgEntities.Models
{
    /// <summary>
    /// Represents a player character in the RPG game.
    /// </summary>
    public class Player
    {
        public string Name { get; set; }
        public string Profession { get; set; }
        public int Level { get; set; }
        public AbilityScores AbilityScores { get; set; } = new();
        public List<Item> Items { get; set; } = new();

        public Player() { }

        public Player(string name, string profession, int level, List<Item> items, AbilityScores abilityScores = null)
        {
            Name = name;
            Profession = profession;
            Level = level;
            Items = items ?? new List<Item>();
            AbilityScores = abilityScores ?? new AbilityScores();
        }

        public override string ToString()
        {
            string itemList = Items != null && Items.Count > 0
                ? string.Join(", ", Items.Select(i => i.Name))
                : "None";
            return $"Name: {Name}\nProfession: {Profession}\nLevel: {Level}\nHealth: {AbilityScores.Health}\nHit Points: {AbilityScores.HitPoints}\nItems: {itemList}\nGold: {AbilityScores.Gold}";
        }

        // TODO: Implement a method or property to calculate the player's total attack value.
        //       This should include base attack (e.g., from level or AbilityScores) plus any bonuses
        //       from equipped items (Items with IsEquipped == true and AttributeModifiers for Attack).
        //       Example signature:
        //       public int GetTotalAttack() { ... }

        // TODO: Implement a method or property to calculate the player's total defense value.
        //       This should include base defense (e.g., from AbilityScores) plus any bonuses
        //       from equipped items (Items with IsEquipped == true and AttributeModifiers for Defense).
        //       Example signature:
        //       public int GetTotalDefense() { ... }

        // TODO: (Optional) Implement logic to use consumable items (e.g., potions) to restore health.
    }
}
