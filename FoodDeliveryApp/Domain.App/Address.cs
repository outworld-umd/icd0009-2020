using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Combined;
using Domain.App.Identity;
using Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App
{
    public class Address : Address<AppUser>, IDomainEntityIdMetadataUser<AppUser>
    {
    }

    public class Address<TUser>: DomainEntityIdMetadataUser<TUser>
        where TUser : IdentityUser<Guid>
    {
        [MinLength(2)] [MaxLength(64)] public string County { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string City { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string Street { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string BuildingNumber { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string? Apartment { get; set; }
        [MinLength(2)] [MaxLength(64)] public string? Name { get; set; }

        [MaxLength(256)] public string? Comment { get; set; }
    }
}