using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{

    public class Address : Address<Guid, AppUser>, IDomainEntityBaseMetadata {
        
    }
    
    public class Address<TKey, TUser> : DomainEntityBaseMetadata<TKey>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey>
    {
        [MinLength(2)] [MaxLength(64)] public string County { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string City { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string Street { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string BuildingNumber { get; set; } = default!;
        [MaxLength(256)] public string Comment { get; set; } = default!;

        
    }
}