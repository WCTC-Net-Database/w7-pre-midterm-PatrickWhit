// Goblin.cs
namespace ConsoleRpgEntities.Models
{
    /// <summary>
    /// Represents a goblin monster.
    /// </summary>
    public class Goblin : MonsterBase
    {
        public string Treasure { get; set; }

        public Goblin() { }

        public Goblin(string name, string type, int level, int health, string treasure)
            : base(name, type, level, health)
        {
            Treasure = treasure;
        }

        /// <summary>
        /// Goblins deal a fixed amount of damage (example: 5).
        /// </summary>
        public override int DealDamage()
        {
            return 5;
        }

        public override string ToString()
        {
            string treasureDisplay = string.IsNullOrEmpty(Treasure) ? "None" : Treasure;
            return base.ToString() + $"\nTreasure: {treasureDisplay}";
        }
    }
}
