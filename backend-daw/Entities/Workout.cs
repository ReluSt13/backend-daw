using fitness_app_backend.Entities;
using Newtonsoft.Json;

namespace backend_daw.Entities
{
    public class Workout
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        [JsonIgnore]
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; }
    }
}
