namespace PublicApi.DTO.DTOs {

    public class PersonDTO : BaseDTO {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalCode { get; set; }
        public int GradeCount { get; set; }
    }

}