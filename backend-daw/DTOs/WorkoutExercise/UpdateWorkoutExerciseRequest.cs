namespace backend_daw.DTOs.WorkoutExercise
{
    public class UpdateWorkoutExerciseRequest
    {
        public int WorkoutExerciseId {  get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
        public double Weight { get; set; }
    }
}
