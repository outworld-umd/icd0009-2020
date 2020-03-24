using System;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain {

    public class Homework : DomainEntity {
        public Guid SubjectGroupId { get; set; }
        public SubjectGroup? SubjectGroup { get; set; }
        public DateTime Added { get; set; }
        public DateTime Deadline { get; set; }
        [ForeignKey(nameof(Teacher))]
        public Guid TeacherId { get; set; }
        public Person? Teacher { get; set; }
    }

}