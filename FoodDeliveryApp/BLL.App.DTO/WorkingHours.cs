using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using Contracts.Domain.Combined;
using Domain.Base;

namespace BLL.App.DTO
{
    public class WorkingHours : WorkingHours<Guid>, IDomainEntityIdMetadata
    {
    }
    
    public class WorkingHours<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        [Required] public DayOfWeek WeekDay { get; set; }
        [Required] public DateTime OpeningTime { get; set; }
        [Required] public DateTime ClosingTime { get; set; }

        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
        
        public TKey Id { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}