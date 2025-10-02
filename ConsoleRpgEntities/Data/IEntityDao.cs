using System.Collections.Generic;

namespace ConsoleRpgEntities.Data
{
    /// <summary>
    /// Generic Data Access Object interface for entities.
    /// </summary>
    public interface IEntityDao<TEntity>
    {
        void Add(TEntity entity);
        TEntity GetByName(string name);
        List<TEntity> GetAll();
    }
}
