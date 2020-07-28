using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using Contracts.Domain;
using Contracts.Domain.Combined;
using Domain.Base;

namespace BLL.App.DTO
{
    public class ItemOption : ItemOption<Guid>, IDomainEntityIdMetadata 
    {
    }

    public class ItemOption<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        [MinLength(2)] [MaxLength(64)] [Required] public string Name { get; set; } = default!;
        [Required] public bool IsRequired { get; set; }
        [Required] public bool IsSingle { get; set; }
        
        public TKey ItemId { get; set; } = default!;
        public Item? Item { get; set; }
        
        public ICollection<ItemChoice>? ItemChoices { get; set; }
        
        public TKey Id { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}