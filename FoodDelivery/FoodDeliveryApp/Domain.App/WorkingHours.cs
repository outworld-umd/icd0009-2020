using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.anguzo.Contracts.Domain;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;
using ee.itcollege.anguzo.Domain.Identity;

namespace Domain.App
{
    public class WorkingHours : WorkingHours<Guid>, IDomainEntityIdMetadata
    {
    }

    public class WorkingHours<TKey> : DomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        [Required] public DayOfWeek WeekDay { get; set; }
        [Required] public DateTime OpeningTime { get; set; }
        [Required] public DateTime ClosingTime { get; set; }
        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
    }
}