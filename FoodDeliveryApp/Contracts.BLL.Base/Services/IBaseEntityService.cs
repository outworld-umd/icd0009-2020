using System;
using System.Linq.Expressions;
using Contracts.DAL.Base.Repositories;
using Contracts.Domain.Base.Basic;

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