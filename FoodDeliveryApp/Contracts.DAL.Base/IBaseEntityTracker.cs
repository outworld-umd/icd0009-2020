using System;
using Contracts.Domain;
using Contracts.Domain.Basic;

namespace Contracts.DAL.Base
{
    public interface IBaseEntityTracker : IBaseEntityTracker<Guid>
    {
        
    }

    public interface IBaseEntityTracker<TKey> where TKey : IEquatable<TKey>
    {
        void AddToEntityTracker(
            IDomainEntityId<TKey> internalEntity,
            IDomainEntityId<TKey> externalEntity);
    }
}