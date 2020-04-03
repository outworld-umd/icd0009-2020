using System;
using Domain;

namespace PublicApi.DTO.DTOs {

    public class GradeDTO : BaseDTO {
        public int Value { get; set; }
        public string TeachersName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TypeName { get; set; }
        public string ColumnDescription { get; set; }
    }

}