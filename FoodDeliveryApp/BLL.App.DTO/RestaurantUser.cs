using System;
using BLL.App.DTO.Identity;
using Contracts.Domain;
using Domain.Base;

namespace BLL.App.DTO
{
    public class RestaurantUser : RestaurantUser<Guid, AppUser>, IDomainBaseEntityMetadata
    {
        
    }
    
    
    public class RestaurantUser<TKey, TUser> : DomainBaseEntityMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public Guid RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }

        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }

    }
}