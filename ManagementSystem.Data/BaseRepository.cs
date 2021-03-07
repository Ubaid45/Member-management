using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace ManagementSystem.Data
{
    public class BaseRepository<TEntity> : IRepository <TEntity> where TEntity : class
    {
        private readonly UsersManagementDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(UsersManagementDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = _dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (includeProperties == null) return orderBy != null ? orderBy(query).ToList() : query.ToList();
                for (var index = 0;
                    index < includeProperties.Split
                        (new char[] {','}, StringSplitOptions.RemoveEmptyEntries).Length;
                    index++)
                {
                    var includeProperty = includeProperties.Split
                        (new char[] {','}, StringSplitOptions.RemoveEmptyEntries)[index];
                    query = query.Include(includeProperty);
                }


                return orderBy != null ? orderBy(query).ToList() : query.ToList();
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
                return null;
            }
            
        }
        
        public TEntity GetById(object id, string includeProperties = "")
        {
            try {
                return _dbSet.Find(id);
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
                return null;
            }
        }

        public virtual void Insert(TEntity entity)
        {
            try
            {
                _dbSet.Add(entity);
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
            }
        }

        public virtual void Delete(object id)
        {
            try
            {
                var entityToDelete = _dbSet.Find(id);
                Delete(entityToDelete);
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
            }
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            try
            {
                if (_context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    _dbSet.Attach(entityToDelete);
                }
                _dbSet.Remove(entityToDelete);
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
            }
            
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            try
            {
                _dbSet.Attach(entityToUpdate);
                _context.Entry(entityToUpdate).State = EntityState.Modified;
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
            }
        }
    }

}