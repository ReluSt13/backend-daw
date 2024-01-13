using backend_daw.Entities;
using Microsoft.AspNetCore.Identity;

namespace fitness_app_backend.Entities
{
    public class User : IdentityUser
    {
        public string? Avatar { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Feedback>? Feedbacks { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
