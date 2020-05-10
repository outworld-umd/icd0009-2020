using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser : AppUser<Guid>
    {
        
    }

    public class AppUser<TKey> : IdentityUser<TKey> 
        where TKey : IEquatable<TKey>
    {

        [MinLength(2)] [MaxLength(64)] public string FirstName { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string LastName { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string Phone { get; set; } = default!;


        public ICollection<Address>? Addresses { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}