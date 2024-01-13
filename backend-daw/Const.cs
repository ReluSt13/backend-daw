namespace backend_daw
{
    public class Const
    {
        public const int UsernameMinLength = 5;

        public const int PostMaxLength = 1024;
        public const string PostLengthValidationError = "Post must have less than 1024 characters.";

        public const string UrlImageRegex =
            @"^(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|jpeg|gif|png|svg)$";

        public const string UrlImageValidationError = "Image must be a valid url.";

        public const string PasswordRegex =
            @"^(?=.*\d{1})(?=.*[a-z]{1})(?=.*[A-Z]{1})(?=.*[!@#$%^&*{|}?~_=+.-]{1})(?=.*[^a-zA-Z0-9])(?!.*\s).{6,24}$";

        public const string UsernameLengthValidationError = "Username must have more than 5 characters.";
        public const string EmailValidationError = "Email must have valid format.";

        public const string PasswordValidationError =
            "Password must have more than 6 characters, min. 1 uppercase, min. 1 lowercase, min. 1 special characters.";

    }
}
