using System.Collections.Generic;
using System.Linq;
using ConsoleRpgEntities.Models;

namespace ConsoleRpgEntities.Data
{
    public class MonsterDao : IEntityDao<MonsterBase>
    {
        private readonly IContext _context;

        public MonsterDao(IContext context)
        {
            _context = context;
        }

        public void Add(MonsterBase monster) => _context.Write(monster);

        public MonsterBase GetByName(string name) => _context.Monsters.FirstOrDefault(m => m.Name == name);

        public List<MonsterBase> GetAll() => _context.Monsters.ToList();

        // Optional: For specific monster types
        public List<Goblin> GetAllGoblins() => _context.Monsters.OfType<Goblin>().ToList();
        public List<Dragon> GetAllDragons() => _context.Monsters.OfType<Dragon>().ToList();
    }
}
