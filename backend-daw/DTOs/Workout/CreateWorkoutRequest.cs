using System.ComponentModel.DataAnnotations;

namespace backend_daw.DTOs.Workout
{
    public class CreateWorkoutRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
