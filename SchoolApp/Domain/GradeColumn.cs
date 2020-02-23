using System;
using System.ComponentModel.DataAnnotations;

namespace Domain {

    public class GradeColumn {
        public int GradeColumnId { get; set; }
        public int GradeTypeId { get; set; }
        public GradeType GradeType { get; set; }
        public DateTime Date { get; set; }
        public int LessonNumber { get; set; }
        [MaxLength(100)]
        public string Theme { get; set; }
        public int SubjectGroupId { get; set; }
        public SubjectGroup SubjectGroup { get; set; }
    }

}