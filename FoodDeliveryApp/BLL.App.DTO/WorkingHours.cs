using System;
using Contracts.Domain;
using Domain.Base;

namespace BLL.App.DTO
{
    public class WorkingHours : WorkingHours<Guid>, IDomainBaseEntityMetadata
    {
        
    }
    
    public class WorkingHours<TKey> : DomainBaseEntityMetadata<TKey>
        where TKey: IEquatable<TKey>
    {
        public DayOfWeek WeekDay { get; set; }
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }

        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
    }
}