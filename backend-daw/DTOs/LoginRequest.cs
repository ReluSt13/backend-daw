using backend_daw;
using System.ComponentModel.DataAnnotations;

namespace fitness_app_backend.DTOs
{
    public class LoginRequest
    {
        [Required]
        [MinLength(Const.UsernameMinLength, ErrorMessage = Const.UsernameLengthValidationError)]
        public string? Username { get; set; }
        [Required]
        [RegularExpression(Const.PasswordRegex, ErrorMessage = Const.PasswordValidationError)]
        public string? Password { get; set; }
    }
}
