using System.Collections.Generic;
using System.Linq;
using Domain;

namespace PublicApi.DTO.DTOs {

    public class PersonDTO : BaseDTO {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalCode { get; set; }
        public List<SubjectDTO> Subjects { get; set; }
        public int SubjectCount { get; set; }
    }

}