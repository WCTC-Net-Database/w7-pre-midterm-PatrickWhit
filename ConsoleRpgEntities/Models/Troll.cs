// Goblin.cs
namespace ConsoleRpgEntities.Models
{
    /// <summary>
    /// Represents a goblin monster.
    /// </summary>
    public class Troll : MonsterBase
    {
        public string Treasure { get; set; }

        public Troll() { }

        public Troll(string name, string type, int level, int health, string treasure)
            : base(name, type, level, health)
        {
            Treasure = treasure;
        }

        /// <summary>
        /// Trolls deal a fixed amount of damage (example: 5).
        /// </summary>
        public override int DealDamage()
        {
            return 12;
        }

        public override string ToString()
        {
            string treasureDisplay = string.IsNullOrEmpty(Treasure) ? "None" : Treasure;
            return base.ToString() + $"\nTreasure: {treasureDisplay}";
        }
    }
}
