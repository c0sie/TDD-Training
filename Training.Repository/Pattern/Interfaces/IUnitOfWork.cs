using System;
using System.Data;

namespace Training.Repository.Pattern.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();

        void BeginTransaction();

        void BeginTransaction(IsolationLevel isolationLevel);

        bool Commit();

        void Rollback();

        void Detach(object o);

        IRepository<T> Repository<T>() where T : class;
    }
}
