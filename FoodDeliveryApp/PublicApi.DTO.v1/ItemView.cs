using System;
using System.Collections.Generic;
using Contracts.Domain.Basic;

namespace PublicApi.DTO.v1
{
    public class ItemView
    {
        public string Name { get; set; } = default!;
        public string? PictureLink { get; set; }
        public decimal Price { get; set; }

        public Guid Id { get; set; } = default!;
    }
}