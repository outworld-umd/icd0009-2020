using System;
using ee.itcollege.anguzo.Contracts.Domain.Base.Basic;
using System.ComponentModel.DataAnnotations;

namespace ee.itcollege.anguzo.DTO.Identity
{
    public class AppUser : AppUser<Guid>
    {
    }
    
    public class AppUser<TKey> : IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>

    {
        public TKey Id { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] [Display(Name = "First Name")] public string FirstName { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] [Display(Name = "Last Name")] public string LastName { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] [Display(Name = "Phone")] public string Phone { get; set; } = default!;

        public string FullName => FirstName + " " + LastName;
    }
}