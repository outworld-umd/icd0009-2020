using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.anguzo.DTO.Identity;
using ee.itcollege.anguzo.Contracts.Domain.Base;

using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;
namespace BLL.App.DTO
{
    public class RestaurantUser : RestaurantUser<Guid, AppUser>, IDomainEntityIdMetadata
    {
    }
    
    
    public class RestaurantUser<TKey, TUser> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public TKey AppUserId { get; set; } = default!;
        [Display(Name = nameof(AppUser), ResourceType = typeof(Resources.BLL.App.DTO.RestaurantUser.RestaurantUser))]
        public TUser? AppUser { get; set; }
    
        public TKey RestaurantId { get; set; } = default!;
        [Display(Name = nameof(Restaurant), ResourceType = typeof(Resources.BLL.App.DTO.RestaurantUser.RestaurantUser))]
        public Restaurant? Restaurant { get; set; }

        [Display(Name = nameof(CreatedBy), ResourceType = typeof(Resources.BLL.App.DTO.RestaurantUser.RestaurantUser))]
        public string? CreatedBy { get; set; }
        [Display(Name = nameof(CreatedAt), ResourceType = typeof(Resources.BLL.App.DTO.RestaurantUser.RestaurantUser))]
        public DateTime CreatedAt { get; set; }
        [Display(Name = nameof(ChangedBy), ResourceType = typeof(Resources.BLL.App.DTO.RestaurantUser.RestaurantUser))]
        public string? ChangedBy { get; set; }
        [Display(Name = nameof(ChangedAt), ResourceType = typeof(Resources.BLL.App.DTO.RestaurantUser.RestaurantUser))]
        public DateTime ChangedAt { get; set; }
    }
}