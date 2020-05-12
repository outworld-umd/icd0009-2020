using System;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain {

    public class RestaurantUser : RestaurantUser<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser> {
        
    }
    
    public class RestaurantUser<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey> {
        
        public Guid RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }

        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }
    }

}