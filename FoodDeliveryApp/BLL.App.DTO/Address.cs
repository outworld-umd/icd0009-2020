using System;
using BLL.App.DTO.Identity;
using Contracts.Domain;
using Domain.Base;

namespace BLL.App.DTO {

    public class Address : Address<Guid, AppUser>, IDomainBaseEntityMetadata
    {
    }
    
    public class Address<TKey, TUser> : DomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public string County { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string BuildingNumber { get; set; } = default!;
        public string? Comment { get; set; } = default!;
        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }
    }

}