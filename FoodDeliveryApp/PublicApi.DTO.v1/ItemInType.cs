using System;
using System.Collections.Generic;
using Contracts.Domain.Basic;

namespace PublicApi.DTO.v1
{
    public class ItemInType
    {
        public Guid Id { get; set; } = default!;

        public Guid ItemTypeId { get; set; } = default!;

        public Guid ItemId { get; set; } = default!;
    }
}