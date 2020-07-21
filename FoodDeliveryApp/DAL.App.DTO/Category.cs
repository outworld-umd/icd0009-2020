using System;
using System.Collections.Generic;
using Contracts.Domain;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO {

        public class Category : Category<Guid, AppUser>, IDomainBaseEntityMetadata
        {
        
        }

        public class Category<TKey, TUser> : DomainEntityIdMetadata<TKey>
            where TKey : IEquatable<TKey>
            where TUser : AppUser<TKey> {

            public string Name { get; set; } = default!;
            public ICollection<RestaurantCategory>? RestaurantCategories { get; set; }
        }

}