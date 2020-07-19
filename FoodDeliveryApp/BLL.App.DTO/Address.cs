using System;
using BLL.App.DTO.Identity;
using Contracts.Domain;

namespace BLL.App.DTO {

    public class Address : Address<Guid, AppUser>, IDomainBaseEntity
    {
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
    }

}