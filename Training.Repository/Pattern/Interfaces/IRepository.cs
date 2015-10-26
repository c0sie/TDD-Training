using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Training.Repository.Pattern.Interfaces
{
    public interface IRepository<TEntity>
    {
        void Insert(TEntity entity);

        void InsertRange(IEnumerable<TEntity> entities);

        void InsertGraphRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void DeleteRange(IEnumerable<TEntity> entities);

        IQueryable<TEntity> Queryable();

        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);

        IQueryFluent<TEntity> Query();

        IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject);

        IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query);

        IRepository<T> GetRepository<T>() where T : class;
    }
}
