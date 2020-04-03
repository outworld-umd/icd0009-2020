using System.Collections.Generic;

namespace PublicApi.DTO.DTOs {

    public class PersonGroupDTO : BaseDTO {
        public string StudentName { get; set; }
        public string SubjectName { get; set; }
        public List<GradeDTO> Grades { get; set; }
    }

}