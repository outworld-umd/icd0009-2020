using System;
using System.Collections.Generic;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class ItemChoice: ItemChoice<Guid>, IDomainBaseEntity
    {
        
    }
    
    public class ItemChoice<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public string Name { get; set; } = default!;
        public decimal AdditionalPrice { get; set; }
        public TKey ItemOptionId { get; set; } = default!;
        public ItemOption? ItemOption { get; set; }
        public ICollection<OrderItemChoice>? OrderItemChoices { get; set; }


    }
}