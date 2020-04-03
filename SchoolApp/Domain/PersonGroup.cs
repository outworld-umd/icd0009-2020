using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain {

    public class PersonGroup : DomainEntity {
        public Guid PersonId { get; set; }
        public Person? Person { get; set; }
        public Guid SubjectGroupId { get; set; }
        public SubjectGroup? SubjectGroup { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        [MaxLength(200)]
        public string Comment { get; set; }
        [InverseProperty(nameof(Grade.StudentGroup))]
        public ICollection<Grade>? Grades { get; set; }

        public string? PersonName => Person?.FirstLastName;
        public string? SubjectName => SubjectGroup?.Subject?.Name;
    }

}