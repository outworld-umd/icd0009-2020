using System;
using Contracts.Domain;

namespace Domain.Base
{
    public abstract class DomainBaseEntityMetadata :  DomainBaseEntityMetadata<Guid>
    {
    }

    public abstract class DomainBaseEntityMetadata<TKey> :  IDomainEntityBaseMetadata<TKey> 
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; } = default!;
        public virtual string? CreatedBy { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string? ChangedBy { get; set; }
        public virtual DateTime ChangedAt { get; set; }
    }

}