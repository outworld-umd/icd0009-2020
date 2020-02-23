using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain {

    public class AbsenceReason {
        public int AbsenceReasonId { get; set; }
        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }
        public Person Student { get; set; }
        [ForeignKey(nameof(Creator))]
        public int CreatorId { get; set; }
        public Person Creator { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public DateTime Created { get; set; }
        [MaxLength(150)]
        public string Text { get; set; }
        public bool IsAccepted { get; set; }
    }

}