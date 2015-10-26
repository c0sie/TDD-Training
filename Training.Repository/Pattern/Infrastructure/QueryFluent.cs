using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Training.Repository.Pattern.Interfaces;

namespace Training.Repository.Pattern.Infrastructure
{
    public sealed class QueryFluent<T> : IQueryFluent<T> where T : class
    {
        private Func<IQueryable<T>, IOrderedQueryable<T>> internalOrderBy;
        private readonly Expression<Func<T, bool>> queryExpression;
        private readonly List<Expression<Func<T, object>>> includes;
        private readonly Repository<T> repository; 

        public QueryFluent(Repository<T> repository)
        {
            this.repository = repository;
            includes = new List<Expression<Func<T, object>>>();
        }

        public QueryFluent(Repository<T> repository, IQueryObject<T> queryObject)
            : this(repository)
        {
            queryExpression = queryObject.Query();
        }

        public QueryFluent(Repository<T> repository, Expression<Func<T, bool>> queryExpression)
            : this(repository)
        {
            this.queryExpression = queryExpression;
        }

        public IQueryFluent<T> OrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> order)
        {
            internalOrderBy = order;

            return this;
        }

        public IQueryFluent<T> Include(Expression<Func<T, object>> exp)
        {
            includes.Add(exp);

            return this;
        }

        public IEnumerable<TResult> Select<TResult>(Expression<Func<T, TResult>> selector)
        {
            return repository.Select(queryExpression, internalOrderBy, includes).Select(selector);
        }

        public IEnumerable<T> Select()
        {
            return repository.Select(queryExpression, internalOrderBy, includes);
        }

        public IQueryable<T> SqlQuery(string query, params object[] parameters)
        {
            return repository.SqlQuery(query, parameters).AsQueryable();
        }
    }
}
