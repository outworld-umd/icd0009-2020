using System;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class WorkingHours : WorkingHours<Guid>, IDomainBaseEntity
    {
        
    }
    
    public class WorkingHours<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; }= default!;
        
        public DayOfWeek WeekDay { get; set; }
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }

        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
    }
}