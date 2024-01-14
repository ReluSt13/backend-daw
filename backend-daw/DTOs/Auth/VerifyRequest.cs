using System.ComponentModel.DataAnnotations;

namespace backend_daw.DTOs.Auth
{
    public class VerifyRequest
    {
        [Required]
        [MinLength(Const.UsernameMinLength, ErrorMessage = Const.UsernameLengthValidationError)]
        public string? Username { get; set; }
    }
}
