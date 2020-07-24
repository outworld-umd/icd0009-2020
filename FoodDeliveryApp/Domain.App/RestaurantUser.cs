using System;
using System.Reflection;
using Contracts.Domain;
using Contracts.Domain.Combined;
using DAL.Base;
using Domain.Base;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;

namespace Domain.App {
    public class RestaurantUser: RestaurantUser<Guid, AppUser>, IDomainEntityIdMetadataUser<AppUser>
    {
    }

    public class RestaurantUser<TKey, TUser> : DomainEntityIdMetadataUser<TUser>  
        where TKey: IEquatable<TKey>
        where TUser : IdentityUser<Guid>
    {
        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
    }
}