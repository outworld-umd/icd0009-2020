using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain;
using Contracts.Domain;
using DAL.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{

    public class Address : Address<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser> {
        
    }
    
    public class Address<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey>
    {
        [MinLength(2)] [MaxLength(64)] public string County { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string City { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string Street { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string BuildingNumber { get; set; } = default!;
        [MaxLength(256)] public string Comment { get; set; } = default!;

        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }
    }
}