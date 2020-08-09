using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base.Combined;
using Domain.Base;

namespace Domain.App
{
    public class ItemOption : ItemOption<Guid>, IDomainEntityIdMetadata 
    {
    }
    
    public class ItemOption<TKey> : DomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        [MinLength(2)] [MaxLength(64)] [Required] public string Name { get; set; } = default!;
        [Required] public bool IsRequired { get; set; }
        [Required] public bool IsSingle { get; set; }
        
        public TKey ItemId { get; set; } = default!;
        public Item? Item { get; set; }
        
        public ICollection<ItemChoice>? ItemChoices { get; set; }
    }
}