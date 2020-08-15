using System;
using System.Threading.Tasks;

namespace ee.itcollege.anguzo.Contracts.DAL.Base
{
    public interface IBaseUnitOfWork
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        
        TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod);
    }
}