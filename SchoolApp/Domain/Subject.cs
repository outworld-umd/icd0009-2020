using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain {
    
    public class Subject : DomainEntity {
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public ICollection<Subject>? Subjects { get; set; }
    }

}