using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using Training.Repository.Pattern.Interfaces;

namespace Training.Repository.Pattern.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> dbSet;
        private readonly DbContext dataContext;
        private readonly IUnitOfWork unitOfWork;

        public Repository(DbContext dataContext, IUnitOfWork unitOfWork)
        {
            this.dataContext = dataContext;
            this.unitOfWork = unitOfWork;
            dbSet = dataContext.Set<TEntity>();
        }

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                dbSet.Add(entity);
            }
        }

        public void InsertGraphRange(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (dataContext.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (dataContext.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }

                dbSet.Remove(entity);
            }
        }

        public IQueryable<TEntity> Queryable()
        {
            return dbSet;
        }

        public IQueryable<TEntity> SqlQuery(string query, params object[] parameters)
        {
            return dbSet.SqlQuery(query, parameters).AsQueryable();
        }

        public IQueryFluent<TEntity> Query()
        {
            return new QueryFluent<TEntity>(this);
        }

        public virtual IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject)
        {
            return new QueryFluent<TEntity>(this, queryObject);
        }

        public virtual IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query)
        {
            return new QueryFluent<TEntity>(this, query);
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return unitOfWork.Repository<T>();
        }

        internal IQueryable<TEntity> Select(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null, int? page = null, int? pageSize = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }

            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query;
        }
    }
}
