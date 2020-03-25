using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [InverseProperty(nameof(AbsenceReason.Student))]
        public ICollection<AbsenceReason>? StudentAbsenceReasons { get; set; }
        [InverseProperty(nameof(AbsenceReason.Creator))]
        public ICollection<AbsenceReason>? CreatedAbsenceReasons { get; set; }
        [InverseProperty(nameof(Dependence.Parent))]
        public ICollection<Dependence>? Children { get; set; }
        [InverseProperty(nameof(Dependence.Child))]
        public ICollection<Dependence>? Parents { get; set; }
        [InverseProperty(nameof(Grade.Student))]
        public ICollection<Grade>? GottenGrades { get; set; }
        [InverseProperty(nameof(Grade.Teacher))]
        public ICollection<Grade>? GivenGrades { get; set; }
        [InverseProperty(nameof(Homework.Teacher))]
        public ICollection<Homework>? GivenHomeworks { get; set; }
        [InverseProperty(nameof(PersonForm.Person))]
        public ICollection<PersonForm>? Forms { get; set; }
        [InverseProperty(nameof(PersonGroup.Person))]
        public ICollection<PersonGroup>? Groups { get; set; }
        [InverseProperty(nameof(Remark.Sender))]
        public ICollection<Remark>? SentRemarks { get; set; }
        [InverseProperty(nameof(Remark.Recipient))]
        public ICollection<Remark>? GottenRemarks { get; set; }

        public string FirstLastName => FirstName + " " + LastName;

    }

}