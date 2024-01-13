using System.ComponentModel.DataAnnotations;

namespace backend_daw.DTOs.Feedback
{
    public class CreateFeedbackRequest
    {
        [Required]
        public bool Value { get; set; }

        [Required]
        public int PostId { get; set; }
    }
}
