using System;
using ee.itcollege.anguzo.Contracts.Domain.Base;

using Contracts.Domain.Base.Basic;

namespace Domain.Base
{
    public abstract class DomainEntityId : DomainEntityId<Guid>, IDomainEntityId
    {
    }
    
    public abstract class DomainEntityId<TKey> : IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; } = default!;
    }
}