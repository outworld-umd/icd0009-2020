﻿using System;
using System.Reflection;
using ee.itcollege.anguzo.Contracts.Domain;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;
using ee.itcollege.anguzo.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Domain.App
{
    public class RestaurantUser : RestaurantUser<Guid, AppUser>, IDomainEntityIdMetadataUser<AppUser>
    {
    }

    public class RestaurantUser<TKey, TUser> : DomainEntityIdMetadataUser<TUser>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<Guid>
    {
        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
    }
}