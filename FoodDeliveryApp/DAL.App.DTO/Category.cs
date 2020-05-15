using System;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO {

        public class Category : Category<Guid, AppUser>, IDomainBaseEntity 
        {
        
        }

        public class Category<TKey, TUser> : IDomainBaseEntity<TKey>
            where TKey : IEquatable<TKey>
            where TUser : AppUser<TKey> {

            public TKey Id { get; set; } = default!;
            public string Name { get; set; } = default!;
        }

}