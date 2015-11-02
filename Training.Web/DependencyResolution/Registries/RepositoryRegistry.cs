using System.Data.Entity;
using StructureMap.Configuration.DSL;
using Training.Entities;
using Training.Repository.Pattern.Infrastructure;
using Training.Repository.Pattern.Interfaces;

namespace Training.Web.DependencyResolution.Registries
{
    public class RepositoryRegistry : Registry
    {
        public RepositoryRegistry()
        {
            For<DbContext>().Use<TrainingDbContext>();
            For<IUnitOfWork>().Use<UnitOfWork>();
            For(typeof(IRepository<>)).Use(typeof(Repository<>));
        }
    }
}
