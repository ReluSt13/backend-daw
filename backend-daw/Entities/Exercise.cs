using Newtonsoft.Json;

namespace backend_daw.Entities
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; }

    }
}
