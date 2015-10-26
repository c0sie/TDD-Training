using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using Moq;
using NUnit.Framework;
using Training.Repository.Mocks;
using Training.Repository.Pattern.Interfaces;

namespace Training.Repository.NUnit
{
    public class TestBase
    {
        protected virtual Mock<IUnitOfWork> MockUnitOfWork { get; private set; }

        [SetUp]
        public virtual void Initialize()
        {
            MockUnitOfWork = new Mock<IUnitOfWork>();
            MockUnitOfWork.Setup(x => x.SaveChanges()).Returns(default(int));
        }

        protected virtual void SetupMockRepository<TEntity>(ICollection<TEntity> mockData) where TEntity : class
        {
            CreateMockRepository(mockData);
        }

        protected virtual void SetupMockRepository<TEntity>() where TEntity : class
        {
            CreateMockRepository(new Collection<TEntity>());
        }

        protected virtual Mock<DbSet<TEntity>> MockSet<TEntity>() where TEntity : class
        {
            return ((MockRepository<TEntity>)MockUnitOfWork.Object.Repository<TEntity>()).MockSet;
        }

        protected virtual void VerifyInsert<TEntity>(Times times) where TEntity : class
        {
            MockSet<TEntity>().Verify(x => x.Add(It.IsAny<TEntity>()), times);
        }

        protected virtual void VerifyInsert<TEntity>() where TEntity : class
        {
            MockSet<TEntity>().Verify(x => x.Add(It.IsAny<TEntity>()));
        }

        protected virtual void VerifyInsert<TEntity>(Expression<Func<TEntity, bool>> match, Times times) where TEntity : class
        {
            MockSet<TEntity>().Verify(x => x.Add(It.Is(match)), times);
        }

        protected virtual void VerifyInsert<TEntity>(Expression<Func<TEntity, bool>> match) where TEntity : class
        {
            MockSet<TEntity>().Verify(x => x.Add(It.Is(match)));
        }

        protected virtual void VerifyUpdate<TEntity>(Times times) where TEntity : class
        {
            MockSet<TEntity>().Verify(x => x.Attach(It.IsAny<TEntity>()), times);
        }

        protected virtual void VerifyUpdate<TEntity>() where TEntity : class
        {
            MockSet<TEntity>().Verify(x => x.Attach(It.IsAny<TEntity>()));
        }

        protected virtual void VerifyUpdate<TEntity>(Expression<Func<TEntity, bool>> match, Times times) where TEntity : class
        {
            MockSet<TEntity>().Verify(x => x.Attach(It.Is(match)), times);
        }

        protected virtual void VerifyUpdate<TEntity>(Expression<Func<TEntity, bool>> match) where TEntity : class
        {
            MockSet<TEntity>().Verify(x => x.Attach(It.Is(match)));
        }

        protected virtual void VerifyDelete<TEntity>(Times times) where TEntity : class
        {
            MockSet<TEntity>().Verify(x => x.Remove(It.IsAny<TEntity>()), times);
        }

        protected virtual void VerifyDelete<TEntity>() where TEntity : class
        {
            MockSet<TEntity>().Verify(x => x.Remove(It.IsAny<TEntity>()));
        }

        protected virtual void VerifyDelete<TEntity>(Expression<Func<TEntity, bool>> match, Times times) where TEntity : class
        {
            MockSet<TEntity>().Verify(x => x.Remove(It.Is(match)), times);
        }

        protected virtual void VerifyDelete<TEntity>(Expression<Func<TEntity, bool>> match) where TEntity : class
        {
            MockSet<TEntity>().Verify(x => x.Remove(It.Is(match)));
        }

        protected virtual void VerifySaveChanges(Times times)
        {
            MockUnitOfWork.Verify(x => x.SaveChanges(), times);
        }

        protected virtual void VerifySaveChanges()
        {
            MockUnitOfWork.Verify(x => x.SaveChanges());
        }

        protected virtual void VerifyBeginTransaction()
        {
            MockUnitOfWork.Verify(x => x.BeginTransaction(), Times.Once());
        }

        protected virtual void VerifyBeginTransaction(Times times)
        {
            MockUnitOfWork.Verify(x => x.BeginTransaction(), times);
        }

        protected virtual void VerifyBeginTransaction(IsolationLevel isolationLevel)
        {
            MockUnitOfWork.Verify(x => x.BeginTransaction(isolationLevel), Times.Once());
        }

        protected virtual void VerifyBeginTransaction(IsolationLevel isolationLevel, Times times)
        {
            MockUnitOfWork.Verify(x => x.BeginTransaction(isolationLevel), times);
        }

        protected virtual void VerifyCommit()
        {
            MockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        protected virtual void VerifyRollback()
        {
            MockUnitOfWork.Verify(x => x.Rollback(), Times.Once);
        }

        private void CreateMockRepository<TEntity>(ICollection<TEntity> mockData) where TEntity : class
        {
            // Create the repository
            var mockRepository = new MockRepository<TEntity>();

            // SetupData sets up the mock db sets
            mockRepository.SetupData(mockData);

            // Attach the mock repository to the mock unit of work
            MockUnitOfWork.Setup(x => x.Repository<TEntity>()).Returns(mockRepository);
        }
    }
}
