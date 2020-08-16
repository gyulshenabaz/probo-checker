using System;
using System.Collections.Generic;

namespace ProboChecker.DataAccess.Repository.Interfaces
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