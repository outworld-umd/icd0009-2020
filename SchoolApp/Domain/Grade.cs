using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain {

    public class Grade {
        public int GradeId { get; set; }
        public int Value { get; set; }
        public bool IsAbsent { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public int AbsenceReasonId { get; set; }
        public AbsenceReason AbsenceReason { get; set; }
        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }
        public Person Student { get; set; }
        [ForeignKey(nameof(Teacher))]
        public int TeacherId { get; set; }
        public Person Teacher { get; set; }
        
    }

}