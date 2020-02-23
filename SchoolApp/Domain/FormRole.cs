using System.ComponentModel.DataAnnotations;

namespace Domain {

    public class FormRole {
        public int FormRoleId { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
    }

}