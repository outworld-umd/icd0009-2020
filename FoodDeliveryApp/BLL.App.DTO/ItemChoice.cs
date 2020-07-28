using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain;
using Contracts.Domain.Combined;
using Domain.Base;

namespace BLL.App.DTO
{
    public class ItemChoice : ItemChoice<Guid>, IDomainEntityIdMetadata 
    {
    }
    
    public class ItemChoice<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        [MinLength(2)] [MaxLength(64)] [Required] public string Name { get; set; } = default!;
        [Column(TypeName = "decimal(6,2)")] [Required] public decimal AdditionalPrice { get; set; }

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