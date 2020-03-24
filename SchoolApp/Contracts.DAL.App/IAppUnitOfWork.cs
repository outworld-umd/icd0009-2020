using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.Base.EF.Repositories;
using Domain;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork {
        public IBaseRepository<DependenceType> DependenceTypes { get; }
        public IBaseRepository<Form> Forms { get; }
        public IBaseRepository<FormRole> FormRoles { get; }
        public IGradeRepository Grades { get; }
        public IBaseRepository<GradeType> GradeTypes { get; }
        public IBaseRepository<Person> Persons { get; }
        public IBaseRepository<RemarkType> RemarkTypes { get; }
        public IBaseRepository<Subject> Subjects { get; }
    }
}