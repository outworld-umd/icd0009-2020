using System;
using Contracts.Domain.Repositories;
using Contracts.Domain.Basic;

namespace Contracts.BLL.Base.Services
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