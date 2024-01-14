using FluentResults;

namespace backend_daw.Services.ExerciseServices
{
    public interface IExerciseService
    {
        Task<Result<string>> GetExercises();
    }
}
