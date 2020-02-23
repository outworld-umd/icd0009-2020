using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain {

    public class Dependence {
        public int DependenceId { get; set; }
        [ForeignKey(nameof(Parent))]
        public int ParentId { get; set; }
        public Person Parent { get; set; }
        [ForeignKey(nameof(Child))] 
        public int ChildId { get; set; }
        public Person Child { get; set; }
        public int DependenceTypeId { get; set; }
        public DependenceType DependenceType { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }

}