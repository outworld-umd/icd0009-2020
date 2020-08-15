using System;
using ee.itcollege.anguzo.Contracts.DAL.Base.Repositories;
using ee.itcollege.anguzo.Contracts.Domain.Base.Basic;

namespace ee.itcollege.anguzo.Contracts.BLL.Base.Services
{
    
    public interface IBaseEntityService<TBLLEntity> : IBaseRepository<Guid, TBLLEntity> 
        where TBLLEntity : class, IDomainEntityId<Guid>, new()
    {
    }

    public interface IBaseEntityService<TKey, TBLLEntity> : IBaseRepository<TKey, TBLLEntity> 
        where TKey : IEquatable<TKey>
        where TBLLEntity : class, IDomainEntityId<TKey>, new()
    {
    }
    
    
}