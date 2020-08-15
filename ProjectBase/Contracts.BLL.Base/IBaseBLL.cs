using System;
using System.Threading.Tasks;

namespace ee.itcollege.anguzo.Contracts.BLL.Base
{
    public interface IBaseBLL
    {
        Task<int> SaveChangesAsync();
        int SaveChanges();
        
        TService GetService<TService>(Func<TService> serviceCreationMethod);
    }
}