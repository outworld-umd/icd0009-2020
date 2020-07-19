using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using DAL.Base;
using Domain.Base;
using Domain.App.Identity;

namespace Domain.App
{
    public class WorkingHours : WorkingHours<Guid, AppUser>, IDomainBaseEntityMetadata {
        
    }
    
    public class WorkingHours<TKey, TUser> : DomainBaseEntityMetadata<TKey>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey>
    {
        public DayOfWeek WeekDay { get; set; }
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }

        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
        
    }
}