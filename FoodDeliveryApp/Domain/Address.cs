using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class Address : DomainEntityBaseMetadata
    {
        [MinLength(2)] [MaxLength(64)] public string County { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string City { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string Street { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string BuildingNumber { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string Apartment { get; set; } = default!;
        [MaxLength(256)] public string Comment { get; set; } = default!;

        [MaxLength(36)] public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public ICollection<OrderAddress>? OrderAddresses { get; set; }
    }
}