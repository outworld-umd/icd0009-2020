using System;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain {

    public class Dependence : DomainEntity {
        [ForeignKey(nameof(Parent))]
        public Guid ParentId { get; set; }
        public Person Parent { get; set; }
        [ForeignKey(nameof(Child))] 
        public Guid ChildId { get; set; }
        public Person Child { get; set; }
        public Guid DependenceTypeId { get; set; }
        public DependenceType DependenceType { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }

}