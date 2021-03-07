using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ManagementSystem.Data
{
    public interface IRepository<TEntity> where TEntity :class
    {
        void Delete(TEntity entityToDelete);
        void Delete(object id);
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            string includeProperties = "");
        TEntity GetById(object id, string includeProperties  = "");
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}