using System;

namespace Domain {

    public class PersonForm {
        public int PersonFormId { get; set; }
        public int FormRoleId { get; set; }
        public FormRole FormRole { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        
    }

}