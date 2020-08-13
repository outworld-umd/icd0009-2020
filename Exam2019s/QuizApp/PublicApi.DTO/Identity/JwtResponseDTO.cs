using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.Identity
{
    public class JwtResponseDTO 
    {
        [Required]
        public string Token { get; set; } = default!;
        [Required]
        public string Status { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public ICollection<string> Roles { get; set; } = default!;
    }
}