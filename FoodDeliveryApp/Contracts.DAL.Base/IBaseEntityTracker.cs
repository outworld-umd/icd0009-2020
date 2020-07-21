using System;
using Contracts.Domain;
using Contracts.Domain.Basic;

namespace Contracts.DAL.Base
{
    public interface IBaseEntityTracker : IBaseEntityTracker<Guid>
    {
        
    }
    // TODO implement notracking where possible

    public interface IBaseEntityTracker<TKey>
    where TKey: IEquatable<TKey>
    {
        //Dictionary<IDomainEntityId<TKey>, IDomainEntityId<TKey>> EntityTracker { get;  }
        void AddToEntityTracker(IDomainEntityId<TKey> internalEntityId, IDomainEntityId<TKey> externalEntityId);
    }
}