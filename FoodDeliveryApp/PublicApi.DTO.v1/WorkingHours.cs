using System;
using Contracts.Domain.Basic;
using Contracts.Domain.Combined;

namespace PublicApi.DTO.v1
{
    public class WorkingHours : WorkingHours<Guid>, IDomainEntityId
    {
    }
    
    public class WorkingHours<TKey> : IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public DayOfWeek WeekDay { get; set; }
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }

        public TKey Id { get; set; } = default!;
    }
}