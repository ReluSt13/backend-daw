using System.ComponentModel.DataAnnotations;

namespace backend_daw.DTOs
{
    public class UpdateAvatarRequest
    {
        [Required]
        public string Avatar { get; set; }
    }
}
