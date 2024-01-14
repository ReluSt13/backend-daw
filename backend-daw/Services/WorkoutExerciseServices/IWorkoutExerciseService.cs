using FluentResults;

namespace backend_daw.Services.WorkoutExerciseServices
{
    public interface IWorkoutExerciseService
    {
        Task<Result<string>> CreateWorkoutExercise(int workouId, int exerciseId, int reps, int sets, double weight);

        Task<Result<string>> UpdateWorkoutExercise(int workoutExerciseId, int? reps, int? sets, double? weight);

        Task<Result<string>> DeleteWorkoutExercise(int workoutExerciseId);

        Task<Result<string>> GetWorkoutExercises(int workoutId);
    }
}
