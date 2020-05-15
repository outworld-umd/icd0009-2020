using System;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO {

    public class Address : Address<Guid, AppUser>, IDomainBaseEntity {
        
    }
    
    public class Address<TKey, TUser> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey> 
    {
        public TKey Id { get; set; } = default!;
        public string County { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string BuildingNumber { get; set; } = default!;
        public string Comment { get; set; } = default!;
        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}
