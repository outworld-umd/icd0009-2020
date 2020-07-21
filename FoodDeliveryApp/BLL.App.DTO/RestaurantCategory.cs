using System;
using Contracts.Domain;
using Domain.Base;

namespace BLL.App.DTO
{
    public class RestaurantCategory : RestaurantCategory<Guid>, IDomainBaseEntityMetadata
    {
        
    }
    
    public class RestaurantCategory<TKey> : DomainEntityIdMetadata<TKey>
        where TKey: IEquatable<TKey>
    {
        
        public TKey CategoryId { get; set; } = default!;
        public Category? Category { get; set; }

        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }

    }
}