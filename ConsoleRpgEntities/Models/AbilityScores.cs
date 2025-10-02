using System.Collections.Generic;

namespace ConsoleRpgEntities.Models
{
    /// <summary>
    /// Represents a set of player ability scores, indexed by Attribute.
    /// This class encapsulates all the core stats for a player, such as Strength, Dexterity, etc.
    /// Each stat is stored in a dictionary and can be accessed via properties or an indexer.
    /// </summary>
    public class AbilityScores
    {
        // The underlying dictionary that maps each Attribute to its integer value.
        // This allows for flexible and dynamic access to any attribute.
        private readonly Dictionary<Attribute, int> _scores = new();

        // ===========================
        // Indexer Explanation
        // ===========================
        // C# allows you to define an "indexer" using the 'this' keyword.
        // This lets you access an instance of the class using array-like syntax:
        // Example: abilityScores[Attribute.Strength] = 10;
        // The indexer below provides a safe way to get/set ability scores by Attribute.
        // It returns 0 if the attribute hasn't been set yet (avoiding exceptions).
        // All the property getters/setters below use this indexer for consistency and safety.

        /// <summary>
        /// Indexer for dynamic access to any attribute.
        /// Example usage:
        ///   abilityScores[Attribute.Strength] = 10;
        ///   int str = abilityScores[Attribute.Strength];
        /// </summary>
        public int this[Attribute attr]
        {
            // Getter: Returns the value for the given attribute, or 0 if not set.
            get => _scores.TryGetValue(attr, out var val) ? val : 0;
            // Setter: Sets the value for the given attribute.
            set => _scores[attr] = value;
        }

        // ===========================
        // Property Usage
        // ===========================
        // Each property below (Strength, Dexterity, etc.) uses the indexer above.
        // This means all access goes through the same logic, making the code easier to maintain.
        // If you ever change how scores are stored or want to add validation, you only need to update the indexer.

        /// <summary>
        /// Physical power and ability to carry heavy objects.
        /// </summary>
        public int Strength
        {
            get => this[Attribute.Strength];
            set => this[Attribute.Strength] = value;
        }

        /// <summary>
        /// Agility, reflexes, and balance.
        /// </summary>
        public int Dexterity
        {
            get => this[Attribute.Dexterity];
            set => this[Attribute.Dexterity] = value;
        }

        /// <summary>
        /// Reasoning and memory.
        /// </summary>
        public int Intelligence
        {
            get => this[Attribute.Intelligence];
            set => this[Attribute.Intelligence] = value;
        }

        /// <summary>
        /// Perception and insight.
        /// </summary>
        public int Wisdom
        {
            get => this[Attribute.Wisdom];
            set => this[Attribute.Wisdom] = value;
        }

        /// <summary>
        /// Force of personality and leadership.
        /// </summary>
        public int Charisma
        {
            get => this[Attribute.Charisma];
            set => this[Attribute.Charisma] = value;
        }

        /// <summary>
        /// Endurance and stamina.
        /// </summary>
        public int Constitution
        {
            get => this[Attribute.Constitution];
            set => this[Attribute.Constitution] = value;
        }

        /// <summary>
        /// Base attack value, used in combat calculations.
        /// </summary>
        public int Attack
        {
            get => this[Attribute.Attack];
            set => this[Attribute.Attack] = value;
        }

        /// <summary>
        /// Base defense value, used in combat calculations.
        /// </summary>
        public int Defense
        {
            get => this[Attribute.Defense];
            set => this[Attribute.Defense] = value;
        }

        /// <summary>
        /// Maximum health points a player can have.
        /// </summary>
        public int Health
        {
            get => this[Attribute.Health];
            set => this[Attribute.Health] = value;
        }

        /// <summary>
        /// Current health points (can be less than or equal to Health).
        /// </summary>
        public int HitPoints
        {
            get => this[Attribute.HitPoints];
            set => this[Attribute.HitPoints] = value;
        }

        /// <summary>
        /// Amount of gold the player possesses.
        /// </summary>
        public int Gold
        {
            get => this[Attribute.Gold];
            set => this[Attribute.Gold] = value;
        }

        /// <summary>
        /// Returns a read-only view of all ability scores.
        /// Useful for iterating over all attributes.
        /// </summary>
        public IReadOnlyDictionary<Attribute, int> AllScores => _scores;
    }
}
