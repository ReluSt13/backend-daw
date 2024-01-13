using fitness_app_backend.Entities;

namespace backend_daw.Entities
{
    public class Comment
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PostId { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
    }
}
