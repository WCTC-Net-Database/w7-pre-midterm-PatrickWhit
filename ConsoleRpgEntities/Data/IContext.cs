using ConsoleRpgEntities.Models;

namespace ConsoleRpgEntities.Data
{
    /// <summary>
    /// Interface for game data context, supporting both players and monsters.
    /// </summary>
    public interface IContext
    {
        List<Player> Players { get; set; }
        List<MonsterBase> Monsters { get; set; }

        void Read();
        void Write(Player player);
        void Write(MonsterBase monster);
        int SaveChanges();
    }
}
