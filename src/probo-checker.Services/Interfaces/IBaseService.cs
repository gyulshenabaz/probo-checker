using System;
using System.Collections.Generic;

namespace probo_checker.Services.Interfaces
{
    public interface IBaseService<TEntity> where TEntity: class, new()
    {
        IEnumerable<TEntity> GetAll(Func<TEntity, bool> filter = null);
        TEntity GetById(int id);
        bool Create(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        bool DeleteRange(IEnumerable<TEntity> entities);
    }
}
