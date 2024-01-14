using System.ComponentModel.DataAnnotations;

namespace backend_daw.DTOs.Workout
{
    public class UpdateWorkoutRequest
    {
        public int WorkoutId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
