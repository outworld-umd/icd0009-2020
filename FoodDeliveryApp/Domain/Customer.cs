using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    //Temporary entity, will be replaced with CustomerUser or smth like that
    public class Customer : DomainEntityBaseMetadata
    {
        [MaxLength(64)]
        [EmailAddress(ErrorMessage = "Wrong email blyat")]
        public string Email { get; set; } = default!;

        [MinLength(2)] [MaxLength(64)] public string FirstName { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string LastName { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string Phone { get; set; } = default!;


        public ICollection<Address>? Addresses { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}