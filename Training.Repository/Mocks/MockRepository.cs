using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using Moq;
using Training.Repository.Pattern.Interfaces;

namespace Training.Repository.Mocks
{
    public class MockRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        // Mocks the DbSet<TEntity> as you would expect to see in the context.
        public Mock<DbSet<TEntity>> MockSet { get; private set; }

        public void Insert(TEntity entity)
        {
            MockSet.Object.Add(entity);
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
        }

        public void InsertGraphRange(IEnumerable<TEntity> entities)
        {
            MockSet.Object.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            MockSet.Object.Attach(entity);
        }

        public void Delete(object id)
        {
            var entity = MockSet.Object.Find(id);

            Delete(entity);
        }

        public void Delete(TEntity entity)
        {
            MockSet.Object.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        public IQueryable<TEntity> Queryable()
        {
            return MockSet.Object;
        }

        public IQueryable<TEntity> SqlQuery(string query, params object[] parameters)
        {
            return MockSet.Object.SqlQuery(query, parameters).AsQueryable();
        }

        public IQueryFluent<TEntity> Query()
        {
            return new MockQueryFluent<TEntity>(this);
        }

        public IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject)
        {
            return new MockQueryFluent<TEntity>(this, queryObject);
        }

        public IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query)
        {
            return new MockQueryFluent<TEntity>(this, query);
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new MockRepository<T>();
        }

        public void SetupData(ICollection<TEntity> mockData)
        {
            MockSet = new Mock<DbSet<TEntity>>().SetupData(mockData);
        }

        internal IQueryable<TEntity> Select(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = MockSet.Object;

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
