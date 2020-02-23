using System;
using System.ComponentModel.DataAnnotations;

namespace Domain {

    public class Person {

        public int PersonId { get; set; }
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string LastName { get; set; }
        [MaxLength(15)]
        public string PersonalCode { get; set; }
        public char Sex { get; set; }
        public DateTime Birthdate { get; set; }
        [MaxLength(30)]
        public string Role { get; set; }
    }

}