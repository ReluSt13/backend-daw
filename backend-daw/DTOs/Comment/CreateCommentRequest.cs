using System.ComponentModel.DataAnnotations;

namespace backend_daw.DTOs.Comment
{
    public class CreateCommentRequest
    {
        [Required]
        [MaxLength(Const.PostMaxLength, ErrorMessage = Const.PostLengthValidationError)]
        public string Content { get; set; }

        [Required]
        public int PostId { get; set; }

    }
}
