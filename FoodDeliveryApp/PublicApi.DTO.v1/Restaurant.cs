using System;
using System.Collections.Generic;
using Contracts.Domain.Basic;

namespace PublicApi.DTO.v1
{
    public class Restaurant : Restaurant<Guid>, IDomainEntityId
    {
    }
    
    
    public class Restaurant<TKey> : IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public string Name { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string? Description { get; set; }
        public decimal DeliveryCost { get; set; }

        public ICollection<WorkingHours>? WorkingHourses { get; set; }
        public ICollection<ItemType>? ItemTypes { get; set; }

        public TKey Id { get; set; } = default!;
    }
}