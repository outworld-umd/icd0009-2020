using System;
using ee.itcollege.anguzo.Contracts.Domain.Base;
using ee.itcollege.anguzo.Contracts.Domain.Base.Basic;

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