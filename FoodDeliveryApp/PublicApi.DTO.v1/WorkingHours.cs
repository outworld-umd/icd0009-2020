using System;
using Contracts.Domain.Basic;
using Contracts.Domain.Combined;

namespace PublicApi.DTO.v1
{
    public class WorkingHours
    {
        public DayOfWeek WeekDay { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }public Guid Id { get; set; } = default!;
    }
}