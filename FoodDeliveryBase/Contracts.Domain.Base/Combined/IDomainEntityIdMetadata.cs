using System;
using ee.itcollege.anguzo.Contracts.Domain.Base.Basic;

namespace ee.itcollege.anguzo.Contracts.Domain.Base.Combined
{
    public interface IDomainEntityIdMetadata : IDomainEntityIdMetadata<Guid>
    {
    }

    public interface IDomainEntityIdMetadata<TKey> : IDomainEntityId<TKey>, IDomainEntityMetadata
        where TKey : IEquatable<TKey>
    {
    }
}