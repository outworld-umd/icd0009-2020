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
        public TKey Id { get; set; } = default!;
        
        
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(WeekDay), ResourceType = typeof(Resources.BLL.App.DTO.WorkingHours.WorkingHours))]
        public DayOfWeek WeekDay { get; set; }
        
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(OpeningTime), ResourceType = typeof(Resources.BLL.App.DTO.WorkingHours.WorkingHours))]
        public DateTime OpeningTime { get; set; }
        
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(ClosingTime), ResourceType = typeof(Resources.BLL.App.DTO.WorkingHours.WorkingHours))]
        public DateTime ClosingTime { get; set; }

        public TKey RestaurantId { get; set; } = default!;
        [Display(Name = nameof(Restaurant), ResourceType = typeof(Resources.BLL.App.DTO.WorkingHours.WorkingHours))]
        public Restaurant? Restaurant { get; set; }
        
        [Display(Name = nameof(CreatedBy), ResourceType = typeof(Resources.BLL.App.DTO.WorkingHours.WorkingHours))]
        public string? CreatedBy { get; set; }
        [Display(Name = nameof(CreatedAt), ResourceType = typeof(Resources.BLL.App.DTO.WorkingHours.WorkingHours))]
        public DateTime CreatedAt { get; set; }
        [Display(Name = nameof(ChangedBy), ResourceType = typeof(Resources.BLL.App.DTO.WorkingHours.WorkingHours))]
        public string? ChangedBy { get; set; }
        [Display(Name = nameof(ChangedAt), ResourceType = typeof(Resources.BLL.App.DTO.WorkingHours.WorkingHours))]
        public DateTime ChangedAt { get; set; }
    }
}