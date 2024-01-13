using fitness_app_backend.Entities;
using Newtonsoft.Json;

namespace backend_daw.Entities
{
    public class Comment
    {
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int PostId { get; set; }
        public User User { get; set; }
        [JsonIgnore]
        public Post Post { get; set; }
    }
}
