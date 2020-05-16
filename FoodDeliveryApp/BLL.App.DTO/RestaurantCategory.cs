using System;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class RestaurantCategory : RestaurantCategory<Guid>, IDomainBaseEntity
    {
        
    }
    
    public class RestaurantCategory<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public TKey CategoryId { get; set; } = default!;
        public Category? Category { get; set; }

        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }

    }
}