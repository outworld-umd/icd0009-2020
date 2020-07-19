using System;
using Contracts.Domain;

namespace Domain.Base
{
    public abstract class DomainEntityMetadata : IDomainEntityMetadata
    {
        public virtual string? CreatedBy { get; set; }
        public virtual DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public virtual string? ChangedBy { get; set; }
        public virtual DateTime ChangedAt { get; set; } = DateTime.Now;
    }
}