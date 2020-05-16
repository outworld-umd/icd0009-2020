using System;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class RestaurantUser : RestaurantUser<Guid>, IDomainBaseEntity
    {
        
    }
    
    
    public class RestaurantUser<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        public Guid RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }

        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }

    }
}