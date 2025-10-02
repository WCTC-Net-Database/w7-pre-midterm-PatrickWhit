namespace ConsoleRpgEntities.Models
{
    /// <summary>
    /// Abstract base class for all monsters.
    /// Provides common properties and methods for monster entities.
    ///
    /// =======================
    /// TPH INHERITANCE & EF CORE
    /// =======================
    /// The 'Type' property below is used to distinguish between different monster subtypes
    /// (e.g., Goblin, Dragon) when loading from JSON. This simulates Table Per Hierarchy (TPH)
    /// inheritance in Entity Framework Core, where all monster types are stored in a single table,
    /// and a discriminator column (like 'Type') identifies the specific derived type.
    ///
    /// In the current file-based approach, 'Type' is used in GameContext to deserialize the correct
    /// monster subclass from JSON. When you transition to EF Core, the database will use a single
    /// 'Monsters' table with a 'Type' column as the discriminator, and EF Core will automatically
    /// instantiate the correct subclass based on this value.
    ///
    /// This design makes it easy to switch from file-based storage to EF Core with TPH mapping.
    /// </summary>
    public abstract class MonsterBase : IMonster
    {
        public string Name { get; set; }
        public int Health { get; set; } // Replaces HP for consistency with IMonster

        /// <summary>
        /// Discriminator property for monster type.
        /// Used to identify the specific subclass (e.g., Goblin, Dragon) when deserializing from JSON,
        /// and will serve as the TPH discriminator column in EF Core.
        /// </summary>
        public string Type { get; set; }
        public int Level { get; set; }

        protected MonsterBase() { }

        protected MonsterBase(string name, string type, int level, int health)
        {
            Name = name;
            Type = type;
            Level = level;
            Health = health;
        }

        /// <summary>
        /// Reduces the monster's health by the specified amount.
        /// </summary>
        /// <param name="amount">Amount of damage to take.</param>
        public virtual void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health < 0)
                Health = 0;
        }

        /// <summary>
        /// Returns the amount of damage this monster deals.
        /// Override in derived classes for custom behavior.
        /// </summary>
        /// <returns>Damage dealt.</returns>
        public abstract int DealDamage();

        public override string ToString()
        {
            return $"Name: {Name}\nType: {Type}\nLevel: {Level}\nHealth: {Health}";
        }
    }
}
