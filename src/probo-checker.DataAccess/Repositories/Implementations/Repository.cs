using Microsoft.EntityFrameworkCore;
using probo_checker.DataAccess.Context;
using probo_checker.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace probo_checker.DataAccess.Repositories.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        private readonly ProboCheckerDbContext context;

        private readonly DbSet<TEntity> set;

        public Repository(ProboCheckerDbContext context)
        {
            this.context = context;
            this.set = this.context.Set<TEntity>();
        }

        public IEnumerable<TEntity> All(Func<TEntity, bool> filter = null)
            => filter == null ? this.set : this.set.Where(filter);
        

        public void Add(TEntity entity)
            => this.set.Add(entity);

        public void Remove(TEntity entity)
            => this.set.Remove(entity);

        public void RemoveRange(IEnumerable<TEntity> entity)
            => this.set.RemoveRange(entity);

        public void Update(TEntity entity)
            => this.set.Update(entity);

        public void SaveChanges()
            => this.context.SaveChanges();

        public void Dispose()
            => this.context.Dispose();

        public TEntity GetById(int id)
            => this.set.Find(id);
    }
}
