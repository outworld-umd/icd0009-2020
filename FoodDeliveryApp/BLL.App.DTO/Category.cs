using System;
using System.Collections.Generic;
using Contracts.Domain;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Category : Category<Guid>, IDomainBaseEntityMetadata
    {
    }
    
    public class Category<TKey> : DomainEntityIdMetadata<TKey>
        where TKey: IEquatable<TKey>
    {
        public string Name { get; set; } = default!;
        public ICollection<RestaurantCategory>? RestaurantCategories { get; set; }
    }
}