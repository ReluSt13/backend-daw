using System.ComponentModel.DataAnnotations;

namespace backend_daw.DTOs.Post
{
    public class UpdatePostRequest
    {
        public int PostId { get; set; }
        [MaxLength(Const.PostMaxLength, ErrorMessage = Const.PostLengthValidationError)]
        public string? Content { get; set; }

        [RegularExpression(Const.UrlImageRegex, ErrorMessage = Const.UrlImageValidationError)]
        public string? Image { get; set; }
    }
}
