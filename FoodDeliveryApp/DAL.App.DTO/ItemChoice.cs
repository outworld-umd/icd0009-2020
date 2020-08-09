using System;
using System.Collections.Generic;
using Contracts.Domain.Base;
using Contracts.Domain.Base.Combined;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class ItemChoice : ItemChoice<Guid>, IDomainEntityIdMetadata 
    {
    }
    
    public class ItemChoice<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public string Name { get; set; } = default!;
        public decimal AdditionalPrice { get; set; }
        public TKey ItemOptionId { get; set; } = default!;
        public ItemOption? ItemOption { get; set; }
        public ICollection<OrderItemChoice>? OrderItemChoices { get; set; }
        
        public TKey Id { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}