using System;
using System.Linq.Expressions;
using LinqKit;
using Training.Repository.Pattern.Interfaces;

namespace Training.Repository.Pattern.Infrastructure
{
    public abstract class QueryObject<TEntity> : IQueryObject<TEntity>
    {
        private Expression<Func<TEntity, bool>> expression;

        public virtual Expression<Func<TEntity, bool>> Query()
        {
            return expression;
        }

        public Expression<Func<TEntity, bool>> And(Expression<Func<TEntity, bool>> query)
        {
            return expression = expression == null ? query : expression.And(query.Expand());
        }

        public Expression<Func<TEntity, bool>> Or(Expression<Func<TEntity, bool>> query)
        {
            return expression = expression == null ? query : expression.Or(query.Expand());
        }

        public Expression<Func<TEntity, bool>> And(IQueryObject<TEntity> queryObject)
        {
            return And(queryObject.Query());
        }

        public Expression<Func<TEntity, bool>> Or(IQueryObject<TEntity> queryObject)
        {
            return Or(queryObject.Query());
        }
    }
}
