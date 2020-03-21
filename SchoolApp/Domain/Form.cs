using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain {

    public class Form : DomainEntity {
        public int Year { get; set; }
        public int FormNumber { get; set; }
        [MaxLength(10)]
        public string Name { get; set; }
    }

}