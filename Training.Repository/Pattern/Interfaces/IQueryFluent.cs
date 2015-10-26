using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Training.Repository.Pattern.Interfaces
{
    public interface IQueryFluent<T>
    {
        IQueryFluent<T> OrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> order);

        IQueryFluent<T> Include(Expression<Func<T, object>> exp);

        IEnumerable<TResult> Select<TResult>(Expression<Func<T, TResult>> selector = null);

        IEnumerable<T> Select();

        IQueryable<T> SqlQuery(string query, params object[] parameters);
    }
}
