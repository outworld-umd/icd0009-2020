using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Basic;
using Contracts.Domain.Combined;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Translation : Translation<Guid>, IDomainEntityIdMetadata
    {
        
    }
    public class Translation<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        [MaxLength(5)] public string Culture { get; set; } = default!;
        [MaxLength(10240)] public string Value { get; set; } = default!;

        public TKey LangStrId { get; set; } = default!;
        public LangStr? LangStr { get; set; }
        
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
        public TKey Id { get; set; }
    }
}