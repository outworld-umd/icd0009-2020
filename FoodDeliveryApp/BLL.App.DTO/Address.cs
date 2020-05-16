using System;
using Contracts.DAL.Base;

namespace BLL.App.DTO {

    public class Address : Address<Guid>, IDomainBaseEntity
    {
    }
    
    public class Address<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        public string County { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string BuildingNumber { get; set; } = default!;
        public string Comment { get; set; } = default!;
        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }
    }

}