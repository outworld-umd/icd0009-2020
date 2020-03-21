using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain {

    public class RemarkType : DomainEntity {
        [MaxLength(30)]
        public string Name { get; set; }
    }

}