using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain {

    public class GradeType : DomainEntity {
        [MaxLength(30)]
        public string Name { get; set; }
        public ICollection<GradeColumn>? GradeColumns { get; set; } 
    }

}