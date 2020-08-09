using System;
using ee.itcollege.anguzo.Contracts.Domain.Base;

using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.DTO.Identity;
using ee.itcollege.anguzo.Domain.Base;
namespace DAL.App.DTO
{
    public class WorkingHours : WorkingHours<Guid>, IDomainEntityIdMetadata
    {
    }
    
    public class WorkingHours<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public DayOfWeek WeekDay { get; set; }
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }
        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
        
        public TKey Id { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}