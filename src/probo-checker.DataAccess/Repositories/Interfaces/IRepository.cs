using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace probo_checker.DataAccess.Repositories.Interfaces
{
    public interface IRepository<TEntity>
       where TEntity : class
    {
        IEnumerable<TEntity> All(Func<TEntity, bool> filter = null);
        TEntity GetById(int id);
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entity);
        void Update(TEntity entity);
        void SaveChanges();
    }
}
