using System.ComponentModel.DataAnnotations;

namespace Domain {

    public class Form {
        public int FormId { get; set; }
        public int Year { get; set; }
        public int FormNumber { get; set; }
        [MaxLength(10)]
        public string Name { get; set; }
    }

}