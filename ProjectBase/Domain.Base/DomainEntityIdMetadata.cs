using System;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;

namespace ee.itcollege.anguzo.Domain.Base
{
    public abstract class DomainEntityIdMetadata :  DomainEntityIdMetadata<Guid>
    {
    }

    public abstract class DomainEntityIdMetadata<TKey> :  IDomainEntityIdMetadata<TKey> 
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; } = default!;
        public virtual string? CreatedBy { get; set; }
        public virtual DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual string? ChangedBy { get; set; }
        public virtual DateTime ChangedAt { get; set; } = DateTime.Now;
    }

}