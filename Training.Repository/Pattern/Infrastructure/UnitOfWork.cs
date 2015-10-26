using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Training.Repository.Pattern.Interfaces;

namespace Training.Repository.Pattern.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed;
        private DbContext dataContext;
        private ObjectContext objectContext;
        private DbTransaction transaction;

        public UnitOfWork(ObjectContext objectContext)
        {
            this.objectContext = objectContext;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                // free other managed objects that implement
                // IDisposable only

                try
                {
                    if (objectContext != null)
                    {
                        if (objectContext.Connection.State == ConnectionState.Open)
                            objectContext.Connection.Close();

                        objectContext.Dispose();
                        objectContext = null;
                    }

                    if (dataContext != null)
                    {
                        dataContext.Dispose();
                        dataContext = null;
                    }
                }
                catch (ObjectDisposedException)
                {
                    // do nothing, the objectContext has already been disposed
                }
            }

            disposed = true;
        }

        public int SaveChanges()
        {
            try
            {
                return dataContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationError in dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                {
                    Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                }

                throw;
            }
        }

        public void BeginTransaction()
        {
            objectContext = ((IObjectContextAdapter)dataContext).ObjectContext;

            if (objectContext.Connection.State != ConnectionState.Open)
            {
                objectContext.Connection.Open();
            }

            transaction = objectContext.Connection.BeginTransaction();
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            objectContext = ((IObjectContextAdapter)dataContext).ObjectContext;

            if (objectContext.Connection.State != ConnectionState.Open)
            {
                objectContext.Connection.Open();
            }

            transaction = objectContext.Connection.BeginTransaction(isolationLevel);
        }

        public bool Commit()
        {
            if (transaction == null)
            {
                return false;
            }

            transaction.Commit();

            return true;
        }

        public void Rollback()
        {
            //if (transaction != null)
            //{
            //    transaction.Rollback();
            //}

            transaction?.Rollback();
        }

        public void Detach(object obj)
        {
            dataContext.Entry(obj).State = EntityState.Detached;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            return ServiceLocator.IsLocationProviderSet ? ServiceLocator.Current.GetInstance<IRepository<T>>() : null;
        }
    }
}
