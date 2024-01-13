using System;
using fitness_app_backend.Entities;

namespace backend_daw.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }
        public string? Image { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<Feedback>? Feedbacks { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}