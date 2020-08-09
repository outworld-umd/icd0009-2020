using System;
using Contracts.Domain.Base.Basic;
using Contracts.Domain.Base.Combined;
namespace PublicApi.DTO.v1
{
    public class WorkingHours
    {
        public DayOfWeek WeekDay { get; set; }
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }public Guid Id { get; set; } = default!;
    }
}