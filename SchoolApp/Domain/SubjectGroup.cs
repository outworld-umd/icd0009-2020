using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain {

    public class SubjectGroup : DomainEntity {
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int Capacity { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
    }

}