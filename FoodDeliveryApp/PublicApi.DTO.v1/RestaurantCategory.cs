using System;

namespace PublicApi.DTO.v1
{
    public class RestaurantCategory
    {
        public Guid CategoryId { get; set; } = default!;

        public Guid RestaurantId { get; set; } = default!;

        public Guid Id { get; set; } = default!;
    }
}