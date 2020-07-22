using System;
using Contracts.Domain.Basic;

namespace PublicApi.DTO.v1
{
    public class RestaurantView : IDomainEntityId
    {
        public string Name { get; set; } = default!;
        public decimal DeliveryCost { get; set; }

        public string[]? Categories { get; set; }
        public bool IsOpen { get; set; }

        public Guid Id { get; set; } = default!;
    }
}