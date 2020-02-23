using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain {

    public class Homework {
        public int HomeworkId { get; set; }
        public int SubjectGroupId { get; set; }
        public SubjectGroup SubjectGroup { get; set; }
        public DateTime Added { get; set; }
        public DateTime Deadline { get; set; }
        [ForeignKey(nameof(Teacher))]
        public int TeacherId { get; set; }
        public Person Teacher { get; set; }
    }

}