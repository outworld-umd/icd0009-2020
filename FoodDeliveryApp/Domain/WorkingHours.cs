using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class WorkingHours : DomainEntityBaseMetadata
    {
        public WeekDay WeekDay { get; set; } = default!;
        public DateTime OpeningTime { get; set; } = default!;
        public DateTime ClosingTime { get; set; } = default!;

        [MaxLength(36)] public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = default!;
    }
}