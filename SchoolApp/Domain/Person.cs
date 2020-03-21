using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain {

    public class Person : DomainEntity {
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