using System.ComponentModel.DataAnnotations;

namespace Domain {

    public class DependenceType {
        public int DependenceTypeId { get; set; }
        [MaxLength(30)]
        public string ParentToChildName { get; set; }
        [MaxLength(30)]
        public string ChildToParentName { get; set; }
    }

}