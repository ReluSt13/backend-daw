﻿using backend_daw;
using System.ComponentModel.DataAnnotations;

namespace backend_daw.DTOs.Auth
{
    public class RegisterRequest
    {
        [Required]
        [MinLength(Const.UsernameMinLength, ErrorMessage = Const.UsernameLengthValidationError)]
        public string? Username { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = Const.EmailValidationError)]
        public string? Email { get; set; }
        [Required]
        [RegularExpression(Const.PasswordRegex, ErrorMessage = Const.PasswordValidationError)]
        public string? Password { get; set; }
    }
}
