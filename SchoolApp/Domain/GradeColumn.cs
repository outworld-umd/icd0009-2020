using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain {

    public class GradeColumn : DomainEntity {
        public Guid GradeTypeId { get; set; }
        public GradeType GradeType { get; set; }
        public DateTime Date { get; set; }
        public int LessonNumber { get; set; }
        [MaxLength(100)]
        public string Theme { get; set; }
        public Guid SubjectGroupId { get; set; }
        public SubjectGroup SubjectGroup { get; set; }
    }

}