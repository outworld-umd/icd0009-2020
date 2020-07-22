using System;
using Contracts.Domain.Basic;

namespace PublicApi.DTO.v1 {

    public class Address : Address<Guid>, IDomainEntityId
    {
    }

    public class Address<TKey> : IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public string County { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string BuildingNumber { get; set; } = default!;
        public string? Comment { get; set; } = default!;

        public TKey Id { get; set; } = default!;

    }

}