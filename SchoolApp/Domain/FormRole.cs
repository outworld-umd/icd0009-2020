using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain {

    public class FormRole : DomainEntity {
        [MaxLength(30)]
        public string Name { get; set; }
        public ICollection<PersonForm>? PersonRoles { get; set; }
    }

}