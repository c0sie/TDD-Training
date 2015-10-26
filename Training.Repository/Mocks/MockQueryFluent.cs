using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Training.Repository.Pattern.Interfaces;

namespace Training.Repository.Mocks
{
    public class MockQueryFluent<T> : IQueryFluent<T> where T : class
    {
        private Func<IQueryable<T>, IOrderedQueryable<T>> orderBy;

        private readonly List<Expression<Func<T, object>>> includes;

        private readonly Expression<Func<T, bool>> expression;

        private readonly MockRepository<T> repository;

        public MockQueryFluent(MockRepository<T> repository)
        {
            this.repository = repository;
            includes = new List<Expression<Func<T, object>>>();
        }

        public MockQueryFluent(MockRepository<T> repository, IQueryObject<T> queryObject)
            : this(repository)
        {
            expression = queryObject.Query();
        }

        public MockQueryFluent(MockRepository<T> repository, Expression<Func<T, bool>> expression)
            : this(repository)
        {
            this.expression = expression;
        }

        public IQueryFluent<T> OrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> order)
        {
            orderBy = order;

            return this;
        }

        public IQueryFluent<T> Include(Expression<Func<T, object>> exp)
        {
            includes.Add(exp);

            return this;
        }

        public IEnumerable<TResult> Select<TResult>(Expression<Func<T, TResult>> selector)
        {
            return repository.Select(expression, orderBy, includes).Select(selector);
        }

        public IEnumerable<T> Select()
        {
            return repository.Select(expression, orderBy, includes);
        }

        public IQueryable<T> SqlQuery(string query, params object[] parameters)
        {
            return repository.SqlQuery(query, parameters).AsQueryable();
        }
    }
}
