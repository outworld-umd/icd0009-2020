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
    }

}