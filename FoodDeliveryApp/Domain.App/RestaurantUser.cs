using System;
using Contracts.Domain;
using DAL.Base;
using Domain.Base;
using Domain.App.Identity;

namespace Domain.App {

    public class RestaurantUser : RestaurantUser<Guid, AppUser>, IDomainBaseEntityMetadata, IDomainEntityUser<AppUser> {
        
    }
    
    public class RestaurantUser<TKey, TUser> : DomainBaseEntityMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey> {
        
        public Guid RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }

        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }
    }

}