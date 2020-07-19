using System;
using System.Threading.Tasks;

namespace Contracts.Domain
{
    public interface IBaseUnitOfWork
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        
        TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod);
    }
}