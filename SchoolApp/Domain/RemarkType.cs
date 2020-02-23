using System.ComponentModel.DataAnnotations;

namespace Domain {

    public class RemarkType {
        public int RemarkTypeId { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
    }

}