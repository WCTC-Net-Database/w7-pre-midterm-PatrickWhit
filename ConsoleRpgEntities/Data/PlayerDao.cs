using System.Collections.Generic;
using System.Linq;
using ConsoleRpgEntities.Models;

namespace ConsoleRpgEntities.Data
{
    public class PlayerDao : IEntityDao<Player>
    {
        private readonly IContext _context;

        public PlayerDao(IContext context)
        {
            _context = context;
        }

        public void Add(Player player) => _context.Write(player);

        public Player GetByName(string name) => _context.Players.FirstOrDefault(p => p.Name == name);

        public List<Player> GetAll() => _context.Players.ToList();
    }
}
