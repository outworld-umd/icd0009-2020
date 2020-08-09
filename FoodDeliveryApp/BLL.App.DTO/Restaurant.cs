using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.anguzo.DTO.Identity;
using ee.itcollege.anguzo.Contracts.Domain.Base;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;
namespace BLL.App.DTO

{
    public class Restaurant : Restaurant<Guid, AppUser>, IDomainEntityIdMetadata
    {
    }
    
    
    public class Restaurant<TKey, TUser> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public TKey Id { get; set; } = default!;

        public TKey AppUserId { get; set; } = default!;
        [Display(Name = "AppUser", ResourceType = typeof(Resources.BLL.App.DTO.Restaurant.Restaurant))]
        public TUser? AppUser { get; set; }
        
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))]
        [MinLength(2, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = "Name", ResourceType = typeof(Resources.BLL.App.DTO.Restaurant.Restaurant))]
        public string Name { get; set; } = default!;
        
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))]
        [MinLength(2, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = "Phone", ResourceType = typeof(Resources.BLL.App.DTO.Restaurant.Restaurant))]
        public string Phone { get; set; } = default!;
        
        [MaxLength(512, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))]
        [MinLength(2, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = "Address", ResourceType = typeof(Resources.BLL.App.DTO.Restaurant.Restaurant))]
        public string Address { get; set; } = default!;
        [MaxLength(512)] 
        [Display(Name = "Description", ResourceType = typeof(Resources.BLL.App.DTO.Restaurant.Restaurant))]
        public string? Description { get; set; }
        [Column(TypeName = "decimal(6,2)")] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = "DeliveryCost", ResourceType = typeof(Resources.BLL.App.DTO.Restaurant.Restaurant))]
        public decimal DeliveryCost { get; set; }
        
        public ICollection<WorkingHours>? WorkingHourses { get; set; }
        public ICollection<RestaurantCategory>? RestaurantCategories { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Item>? Items { get; set; }
        public ICollection<ItemType>? ItemTypes { get; set; }
        public ICollection<RestaurantUser>? RestaurantUsers { get; set; }

        [Display(Name = nameof(CreatedBy), ResourceType = typeof(Resources.BLL.App.DTO.Restaurant.Restaurant))]
        public string? CreatedBy { get; set; }
        [Display(Name = nameof(CreatedAt), ResourceType = typeof(Resources.BLL.App.DTO.Restaurant.Restaurant))]
        public DateTime CreatedAt { get; set; }
        [Display(Name = nameof(ChangedBy), ResourceType = typeof(Resources.BLL.App.DTO.Restaurant.Restaurant))]
        public string? ChangedBy { get; set; }
        [Display(Name = nameof(ChangedAt), ResourceType = typeof(Resources.BLL.App.DTO.Restaurant.Restaurant))]
        public DateTime ChangedAt { get; set; }
    }
}