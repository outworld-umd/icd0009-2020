using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain {

    public class PersonGroup : DomainEntity {
        public Guid PersonId { get; set; }
        public Person? Person { get; set; }
        public Guid SubjectGroupId { get; set; }
        public SubjectGroup? SubjectGroup { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        [MaxLength(200)]
        public string Comment { get; set; }
    }

}