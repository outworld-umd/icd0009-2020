using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.Base.EF.Repositories;
using Domain;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork {
        public IGradeRepository Grades { get; }
        public IBaseRepository<Person> Persons { get; }
    }
}