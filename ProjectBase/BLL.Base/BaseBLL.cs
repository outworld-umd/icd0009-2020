using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.anguzo.Contracts.BLL.Base;
using ee.itcollege.anguzo.Contracts.DAL.Base;

namespace ee.itcollege.anguzo.BLL.Base
{
    public class BaseBLL<TUnitOfWork> : IBaseBLL
        where TUnitOfWork: IBaseUnitOfWork
    {

        protected readonly TUnitOfWork UnitOfWork;
        
        public BaseBLL(TUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        
        public int SaveChanges()
        {
            return UnitOfWork.SaveChanges();
        }
        
        public async Task<int> SaveChangesAsync()
        {
            return await UnitOfWork.SaveChangesAsync();
        }

        private readonly Dictionary<Type, object> _repoCache = new Dictionary<Type, object>();

        // Factory method
        public TService GetService<TService>(Func<TService> serviceCreationMethod)
        {
            if (_repoCache.TryGetValue(typeof(TService), out var repo))
            {
                return (TService) repo;
            }

            repo = serviceCreationMethod()!;
            _repoCache.Add(typeof(TService), repo);
            return (TService) repo;
        }
    }
}