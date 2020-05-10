using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    
    public class RestaurantCategory : RestaurantCategory<Guid, AppUser>, IDomainEntityBaseMetadata {
        
    }
    
    public class RestaurantCategory<TKey, TUser> : DomainEntityBaseMetadata<TKey>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey> {
        public TKey CategoryId { get; set; } = default!;
        public Category? Category { get; set; }

        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
        
    }
}