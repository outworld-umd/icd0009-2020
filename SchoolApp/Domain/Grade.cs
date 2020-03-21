using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain {

    public class Grade : DomainEntity {
        public int Value { get; set; }
        public bool IsAbsent { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public Guid? AbsenceReasonId { get; set; }
        public AbsenceReason? AbsenceReason { get; set; }
        [ForeignKey(nameof(Student))]
        public Guid StudentId { get; set; }
        public Person Student { get; set; }
        [ForeignKey(nameof(Teacher))]
        public Guid TeacherId { get; set; }
        public Person Teacher { get; set; }
        
    }

}