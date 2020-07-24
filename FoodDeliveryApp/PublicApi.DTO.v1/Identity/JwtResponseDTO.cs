using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.Identity
{
    public class JwtResponseDTO
    {
        [Required]
        public string Token { get; set; } = default!;
        [Required]
        public string Status { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}