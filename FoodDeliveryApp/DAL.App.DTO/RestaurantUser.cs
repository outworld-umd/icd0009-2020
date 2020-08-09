using System;
using ee.itcollege.anguzo.Contracts.Domain.Base;

using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.DTO.Identity;
using ee.itcollege.anguzo.Domain.Base;
namespace DAL.App.DTO
{
    public class RestaurantUser : RestaurantUser<Guid, AppUser>, IDomainEntityIdMetadata
    {
    }
    
    
    public class RestaurantUser<TKey, TUser> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }
    
        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }

        public TKey Id { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}