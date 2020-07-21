using System;
using Contracts.Domain;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class RestaurantCategory : RestaurantCategory<Guid, AppUser>, IDomainBaseEntityMetadata
    {
        
    }
    
    public class RestaurantCategory<TKey, TUser> : DomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        
        public TKey CategoryId { get; set; } = default!;
        public Category? Category { get; set; }

        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }

    }
}