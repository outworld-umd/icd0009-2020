using System.Linq;
using Domain;
using PublicApi.DTO.DTOs;

namespace PublicApi.DTO {

    public static class DTOFactory {
        
        public static PersonDTO GetPersonDTOForList(Person person) {
            return new PersonDTO {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                PersonalCode = person.PersonalCode,
                SubjectCount = person.Groups.Count
            };
        }

        public static PersonDTO GetPersonDTOSingle(Person person) {
            return new PersonDTO {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                PersonalCode = person.PersonalCode,
                SubjectCount = person.Groups.Count,
                Subjects = person.Groups.Select(a => new SubjectDTO {
                        Id = a.SubjectGroup.Id, 
                        Name = a.SubjectGroup.Subject.Name
                }).ToList()
            };
        }
        
        public static PersonGroupDTO GetPersonGroupDTOForList(PersonGroup personGroup) {
            return new PersonGroupDTO {
                Id = personGroup.Id,
                StudentName = personGroup.PersonName,
                SubjectName = personGroup.SubjectName
            };
        }

        public static PersonGroupDTO GetPersonGroupDTOSingle(PersonGroup personGroup) {
            return new PersonGroupDTO {
                Id = personGroup.Id,
                StudentName = personGroup.PersonName,
                SubjectName = personGroup.SubjectName,
                Grades = personGroup.Grades.Select(a => new GradeDTO {
                    Id = a.Id,
                    CreatedAt = a.CreatedAt,
                    Value = a.Value,
                    TeachersName = a.TeachersName
                }).ToList()
            };
        }

        public static GradeDTO GetGradeDTO(Grade grade) {
            return new GradeDTO {
                Id = grade.Id,
                ColumnDescription = grade.GradeColumn.Theme,
                CreatedAt = grade.CreatedAt,
                TeachersName = grade.TeachersName,
                TypeName = grade.GradeColumn.GradeType.Name,
                Value = grade.Value
            };
        }
    }

}