using FluentResults;

namespace backend_daw.Services.WorkoutServices
{
    public interface IWorkoutService
    {
        Task<Result<string>> CreateWorkout(string userId, string name);

        Task<Result<string>> UpdateWorkout(int workoutId, string name);

        Task<Result<string>> DeleteWorkout(int workoutId);

        Task<Result<string>> GetWorkouts(string userId);
    }
}
