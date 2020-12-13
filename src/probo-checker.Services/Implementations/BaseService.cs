using probo_checker.DataAccess.Repositories.Interfaces;
using probo_checker.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace probo_checker.Services.Implementations
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity: class, new()
    {
        protected readonly IRepository<TEntity> repository;

        public BaseService(IRepository<TEntity> repository)
        {
            this.repository = repository;
        }
        public IEnumerable<TEntity> GetAll(Func<TEntity, bool> filter = null)
        {            
            return repository.All(filter);
        }

        public TEntity GetById(int id)
        {
            return repository.GetById(id);
        }

        public bool Create(TEntity entity)
        {
            IEnumerable<TEntity> entities = repository.All();
            PropertyInfo entityPropertyInfo = entity.GetType().GetProperty("Id");
            foreach (var item in entities)
            {
                PropertyInfo itemPropertyInfo = item.GetType().GetProperty("Id");
                entityPropertyInfo.SetValue(entity, itemPropertyInfo.GetValue(item));
                if (item == entity) return true;
            }

            try
            {
                repository.Add(entity);
                repository.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool Delete(TEntity entity)
        {
            try
            {
                repository.Remove(entity);
                repository.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteRange(IEnumerable<TEntity> entities)
        {
            try
            {
                repository.RemoveRange(entities);
                repository.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }


        public bool Update(TEntity entity)
        {
            try
            {
                repository.Update(entity);
                repository.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        protected bool IsEntityStateValid(object model)
        {
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(model, validationContext, validationResults,
                validateAllProperties: true);
        }
    }
}
