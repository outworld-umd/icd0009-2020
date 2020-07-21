using System;
using Contracts.Domain;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class RestaurantUser : RestaurantUser<Guid, AppUser>, IDomainBaseEntityMetadata
    {
        
    }
    
    
    public class RestaurantUser<TKey, TUser> : DomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public Guid RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }

        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }

    }
}