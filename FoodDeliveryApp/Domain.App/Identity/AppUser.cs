using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{
    public class AppUser : AppUser<Guid> {
        
    }

    public class AppUser<TKey> : IdentityUser<TKey> 
        where TKey : IEquatable<TKey>
    {

        [MinLength(2)] [MaxLength(64)] [Display(Name = "First Name")] public string FirstName { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] [Display(Name = "Last Name")] public string LastName { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] [Display(Name = "Phone")] public string Phone { get; set; } = default!;

        public string FullName => FirstName + " " + LastName;

    }
}