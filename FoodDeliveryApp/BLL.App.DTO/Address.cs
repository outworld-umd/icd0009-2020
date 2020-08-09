using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
namespace BLL.App.DTO {

    public class Address : Address<Guid, AppUser>, IDomainEntityIdMetadata
    {
    }

    public class Address<TKey, TUser> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public TKey AppUserId { get; set; } = default!;
        [Display(Name = "AppUser", ResourceType = typeof(Resources.BLL.App.DTO.Address.Address))]
        public TUser? AppUser { get; set; }
        
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [MinLength(2, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))]
        [Display(Name = nameof(County), ResourceType = typeof(Resources.BLL.App.DTO.Address.Address))]
        public string County { get; set; } = default!;
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [MinLength(2, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(City), ResourceType = typeof(Resources.BLL.App.DTO.Address.Address))]
        public string City { get; set; } = default!;
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [MinLength(2, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(Street), ResourceType = typeof(Resources.BLL.App.DTO.Address.Address))]
        public string Street { get; set; } = default!;
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [MinLength(2, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(BuildingNumber), ResourceType = typeof(Resources.BLL.App.DTO.Address.Address))]
        public string BuildingNumber { get; set; } = default!;
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(Apartment), ResourceType = typeof(Resources.BLL.App.DTO.Address.Address))]
        public string? Apartment { get; set; }
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [MinLength(2, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.BLL.App.DTO.Address.Address))]
        public string Name { get; set; }= default!;
        [MaxLength(256, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(Comment), ResourceType = typeof(Resources.BLL.App.DTO.Address.Address))]
        public string? Comment { get; set; }

        [Display(Name = nameof(CreatedBy), ResourceType = typeof(Resources.BLL.App.DTO.Address.Address))]
        public string? CreatedBy { get; set; }
        [Display(Name = nameof(CreatedAt), ResourceType = typeof(Resources.BLL.App.DTO.Address.Address))]
        public DateTime CreatedAt { get; set; }
        [Display(Name = nameof(ChangedBy), ResourceType = typeof(Resources.BLL.App.DTO.Address.Address))]
        public string? ChangedBy { get; set; }
        [Display(Name = nameof(ChangedAt), ResourceType = typeof(Resources.BLL.App.DTO.Address.Address))]
        public DateTime ChangedAt { get; set; }
    }

}