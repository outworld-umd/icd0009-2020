using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain {

    public class SubjectGroup : DomainEntity {
        public Guid SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public int Capacity { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        public ICollection<GradeColumn>? GradeColumns { get; set; }
        public ICollection<Homework>? Homeworks { get; set; }
        public ICollection<PersonGroup>? Persons { get; set; }
    }

}