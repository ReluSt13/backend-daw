using Newtonsoft.Json;

namespace backend_daw.Entities
{
    public class WorkoutExercise
    {
        public int Id { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
        public double Weight { get; set; }
        public int WorkoutId { get; set; }
        public int ExerciseId { get; set; }
        [JsonIgnore]
        public Workout Workout { get; set; }
        [JsonIgnore]
        public Exercise Exercise { get; set; }
    }
}
