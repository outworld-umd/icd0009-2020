using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BLL.App.DTO.Identity;
using ee.itcollege.anguzo.Contracts.Domain.Base;

using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using Domain.App.Enums;
using ee.itcollege.anguzo.Domain.Base;
namespace BLL.App.DTO
{
    public class Order : Order<Guid, AppUser>, IDomainEntityIdMetadata
    {
    }

    public class Order<TKey, TUser> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public TKey Id { get; set; } = default!;

        public TKey AppUserId { get; set; } = default!;
        [Display(Name = "AppUser", ResourceType = typeof(Resources.BLL.App.DTO.Order.Order))]
        public TUser? AppUser { get; set; }
        
        
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(OrderStatus), ResourceType = typeof(Resources.BLL.App.DTO.Order.Order))]
        public OrderStatus OrderStatus { get; set; }
        
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(PaymentMethod), ResourceType = typeof(Resources.BLL.App.DTO.Order.Order))]
        public PaymentMethod PaymentMethod { get; set; }

        [Column(TypeName = "decimal(6,2)")] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(FoodCost), ResourceType = typeof(Resources.BLL.App.DTO.Order.Order))]
        public decimal FoodCost { get; set; }
        [Column(TypeName = "decimal(6,2)")] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(DeliveryCost), ResourceType = typeof(Resources.BLL.App.DTO.Order.Order))]
        public decimal DeliveryCost { get; set; }
        
        [MaxLength(512, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))]
        [MinLength(2, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(Address), ResourceType = typeof(Resources.BLL.App.DTO.Order.Order))]
        public string Address { get; set; } = default!;
        
        [MaxLength(512, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))]
        [MinLength(2, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(Apartment), ResourceType = typeof(Resources.BLL.App.DTO.Order.Order))]
        public string? Apartment { get; set; }

        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))]
        [MinLength(2, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(RestaurantName), ResourceType = typeof(Resources.BLL.App.DTO.Order.Order))]
        public string? RestaurantName { get; set; }
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))]
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.BLL.App.DTO.Order.Order))]
        public string? Name { get; set; }
        
        [MaxLength(512, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))]
        [Display(Name = nameof(Comment), ResourceType = typeof(Resources.BLL.App.DTO.Order.Order))]
        public string? Comment { get; set; }
        



        public Guid? RestaurantId { get; set; }
        [Display(Name = nameof(Restaurant), ResourceType = typeof(Resources.BLL.App.DTO.Order.Order))]
        public Restaurant? Restaurant { get; set; }

        public ICollection<OrderRow>? OrderRows { get; set; }

        [Display(Name = nameof(CreatedBy), ResourceType = typeof(Resources.BLL.App.DTO.Order.Order))]
        public string? CreatedBy { get; set; }
        [Display(Name = nameof(CreatedAt), ResourceType = typeof(Resources.BLL.App.DTO.Order.Order))]
        public DateTime CreatedAt { get; set; }
        [Display(Name = nameof(ChangedBy), ResourceType = typeof(Resources.BLL.App.DTO.Order.Order))]
        public string? ChangedBy { get; set; }
        [Display(Name = nameof(ChangedAt), ResourceType = typeof(Resources.BLL.App.DTO.Order.Order))]
        public DateTime ChangedAt { get; set; }
    }
}