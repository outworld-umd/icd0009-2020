using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain {

    public class DependenceType : DomainEntity  {
        [MaxLength(30)]
        public string ParentToChildName { get; set; }
        [MaxLength(30)]
        public string ChildToParentName { get; set; }
        public ICollection<Dependence>? Dependences { get; set; }
    }

}