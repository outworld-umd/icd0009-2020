using System;
using System.Collections.Generic;
using Contracts.Domain;
using Contracts.Domain.Combined;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class ItemOption : ItemOption<Guid>, IDomainEntityIdMetadata 
    {
    }

    public class ItemOption<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public string Name { get; set; } = default!;
        public bool IsRequired { get; set; }
        public bool IsSingle { get; set; }


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