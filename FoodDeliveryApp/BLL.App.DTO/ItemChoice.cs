using System;
using System.Collections.Generic;
using Contracts.Domain;
using Domain.Base;

namespace BLL.App.DTO
{
    public class ItemChoice: ItemChoice<Guid>, IDomainBaseEntityMetadata
    {
        
    }
    
    public class ItemChoice<TKey> : DomainBaseEntityMetadata<TKey>
        where TKey: IEquatable<TKey>
    {
        
        public string Name { get; set; } = default!;
        public decimal AdditionalPrice { get; set; }
        public TKey ItemOptionId { get; set; } = default!;
        public ItemOption? ItemOption { get; set; }
        public ICollection<OrderItemChoice>? OrderItemChoices { get; set; }


    }
}