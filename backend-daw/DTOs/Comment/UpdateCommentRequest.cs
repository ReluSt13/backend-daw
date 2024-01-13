using System.ComponentModel.DataAnnotations;

namespace backend_daw.DTOs.Comment
{
    public class UpdateCommentRequest
    {
        public int PostId { get; set; }

        [Required]
        [MaxLength(Const.PostMaxLength, ErrorMessage = Const.PostLengthValidationError)]
        public string? Content { get; set; }
    }
}
