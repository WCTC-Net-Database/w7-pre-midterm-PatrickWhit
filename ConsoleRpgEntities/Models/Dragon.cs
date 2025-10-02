// Dragon.cs
namespace ConsoleRpgEntities.Models
{
    /// <summary>
    /// Represents a dragon monster.
    /// </summary>
    public class Dragon : MonsterBase
    {
        public int FirePower { get; set; }
        public string Element { get; set; }

        public Dragon() { }

        public Dragon(string name, string type, int level, int health, int firePower, string element)
            : base(name, type, level, health)
        {
            FirePower = firePower;
            Element = element;
        }

        /// <summary>
        /// Dragons deal damage based on their FirePower (example: FirePower * 2).
        /// </summary>
        public override int DealDamage()
        {
            return FirePower * 2;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nFirePower: {FirePower}\nElement: {Element}";
        }
    }
}
