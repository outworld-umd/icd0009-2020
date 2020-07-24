using System;
using Contracts.Domain.Basic;

namespace PublicApi.DTO.v1 {

    public class Address
    {
        public string County { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string BuildingNumber { get; set; } = default!;
        public string? Comment { get; set; }
        public string? Name { get; set; }

        public Guid Id { get; set; } = default!;

    }

}