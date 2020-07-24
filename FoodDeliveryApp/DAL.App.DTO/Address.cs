using System;
using Contracts.Domain;
using Contracts.Domain.Basic;
using Contracts.Domain.Combined;
using DAL.App.DTO.Identity;
using Domain.App;
using Domain.Base;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace DAL.App.DTO
{
    public class Address : Address<Guid, AppUser>, IDomainEntityIdMetadata
    {
    }

    public class Address<TKey, TUser> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }

        public string County { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string BuildingNumber { get; set; } = default!;
        public string? Apartment { get; set; }
        public string Name { get; set; } = default!;
        public string? Comment { get; set; }
        
        public TKey Id { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}