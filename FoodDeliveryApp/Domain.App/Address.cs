using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;
using ee.itcollege.anguzo.Domain.Identity;using Microsoft.AspNetCore.Identity;

namespace Domain.App
{
    public class Address : Address<AppUser>, IDomainEntityIdMetadataUser<AppUser>
    {
    }

    public class Address<TUser>: DomainEntityIdMetadataUser<TUser>
        where TUser : IdentityUser<Guid>
    {
        [MaxLength(64)] [MinLength(2)] [Required] public string County { get; set; } = default!;
        [MaxLength(64)] [MinLength(2)] [Required] public string City { get; set; } = default!;
        [MaxLength(64)] [MinLength(2)] [Required] public string Street { get; set; } = default!;
        [MaxLength(64)] [MinLength(2)] [Required] public string BuildingNumber { get; set; } = default!;
        [MaxLength(64)] public string? Apartment { get; set; }
        [MaxLength(64)] [MinLength(2)] [Required] public string Name { get; set; }= default!;
        [MaxLength(256)] public string? Comment { get; set; }
    }
}