namespace backend_daw.DTOs.WorkoutExercise
{
    public class CreateWorkoutExerciseRequest
    {
        public int Reps { get; set; }
        public int Sets { get; set; }
        public double Weight { get; set; }
        public int WorkoutId { get; set; }
        public int ExerciseId { get; set; }
    }
}
