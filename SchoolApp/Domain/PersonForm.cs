using System;
using DAL.Base;

namespace Domain {

    public class PersonForm : DomainEntity {
        public Guid FormId { get; set; }
        public Form? Form { get; set; }
        public Guid FormRoleId { get; set; }
        public FormRole? FormRole { get; set; }
        public Guid PersonId { get; set; }
        public Person? Person { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        
    }

}