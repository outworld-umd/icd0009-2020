using System.ComponentModel.DataAnnotations;

namespace Domain {

    public class Subject {
        public int SubjectId { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
    }

}