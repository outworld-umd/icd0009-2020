using System;

namespace ee.itcollege.anguzo.Contracts.Domain.Base.Basic
{
    public interface IDomainEntityId : IDomainEntityId<Guid>
    {
    }

    public interface IDomainEntityId<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}