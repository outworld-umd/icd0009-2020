using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain {

    public class AbsenceReason : DomainEntity {
        [ForeignKey(nameof(Student))]
        public Guid StudentId { get; set; }
        public Person? Student { get; set; }
        [ForeignKey(nameof(Creator))]
        public Guid CreatorId { get; set; }
        public Person? Creator { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public DateTime Created { get; set; }
        [MaxLength(150)]
        public string Text { get; set; }
        public bool IsAccepted { get; set; }
    }

}