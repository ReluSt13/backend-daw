﻿using fitness_app_backend.Entities;

namespace backend_daw.Entities
{
    public class Feedback
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool Value { get; set; }
        public string PostId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
    }
}
