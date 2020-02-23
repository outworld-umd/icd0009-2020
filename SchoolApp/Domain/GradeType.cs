using System.ComponentModel.DataAnnotations;

namespace Domain {

    public class GradeType {
        public int GradeTypeId { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
    }

}