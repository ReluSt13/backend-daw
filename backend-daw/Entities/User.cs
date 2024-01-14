using backend_daw.Entities;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace fitness_app_backend.Entities
{
    public class User : IdentityUser
    {
        public string? Avatar { get; set; }
        [JsonIgnore]
        public ICollection<Post>? Posts { get; set; }
        [JsonIgnore]
        public ICollection<Feedback>? Feedbacks { get; set; }
        [JsonIgnore]
        public ICollection<Comment>? Comments { get; set; }
        [JsonIgnore]
        public ICollection<Workout>? Workouts { get; set; }
    }
}

