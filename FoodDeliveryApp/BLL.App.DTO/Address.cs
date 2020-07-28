using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using Contracts.Domain;
using Contracts.Domain.Combined;
using Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace BLL.App.DTO {

    public class Address : Address<Guid, AppUser>, IDomainEntityIdMetadata
    {
    }

    public class Address<TKey, TUser> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }
        
        [MaxLength(64)] [MinLength(2)] [Required] public string County { get; set; } = default!;
        [MaxLength(64)] [MinLength(2)] [Required] public string City { get; set; } = default!;
        [MaxLength(64)] [MinLength(2)] [Required] public string Street { get; set; } = default!;
        [MaxLength(64)] [MinLength(2)] [Required] public string BuildingNumber { get; set; } = default!;
        [MaxLength(64)] public string? Apartment { get; set; }
        [MaxLength(64)] [MinLength(2)] [Required] public string Name { get; set; }= default!;
        [MaxLength(256)] public string? Comment { get; set; }
        public TKey Id { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }

}