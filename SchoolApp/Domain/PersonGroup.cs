using System;
using System.ComponentModel.DataAnnotations;

namespace Domain {

    public class PersonGroup {
        public int PersonGroupId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int SubjectGroupId { get; set; }
        public SubjectGroup SubjectGroup { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        [MaxLength(200)]
        public string Comment { get; set; }
    }

}