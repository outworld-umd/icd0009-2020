using System.ComponentModel.DataAnnotations;

namespace Domain {

    public class SubjectGroup {
        public int SubjectGroupId { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int Capacity { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
    }

}