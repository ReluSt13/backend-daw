using System.ComponentModel.DataAnnotations;

namespace backend_daw.DTOs.Avatar
{
    public class UpdateAvatarRequest
    {
        [Required]
        public string Avatar { get; set; }
    }
}
