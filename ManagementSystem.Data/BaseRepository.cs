using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using ManagementSystem.Data.Interfaces;

namespace ManagementSystem.Data
{
    public class BaseRepository<TEntity> : IBaseRepository <TEntity> where TEntity : class
    {
        protected readonly UsersManagementDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(UsersManagementDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = DbSet;

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
                return DbSet.Find(id);
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
                DbSet.Add(entity);
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
                var entityToDelete = DbSet.Find(id);
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
                if (Context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    DbSet.Attach(entityToDelete);
                }
                DbSet.Remove(entityToDelete);
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
                DbSet.Attach(entityToUpdate);
                Context.Entry(entityToUpdate).State = EntityState.Modified;
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
            }
        }
    }

}