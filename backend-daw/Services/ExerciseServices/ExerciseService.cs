using fitness_app_backend.Db;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace backend_daw.Services.ExerciseServices
{
    public class ExerciseService : IExerciseService
    {
        private readonly AppDbContext _dbContext;

        public ExerciseService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<string>> GetExercises()
        {
            try
            {
                var allExercises = _dbContext.Exercises.ToList();

                if (allExercises == null || !allExercises.Any())
                {
                    return Result.Fail<string>("No exercise found.");
                }

                var exercisesJson = JsonConvert.SerializeObject(allExercises);
                return Result.Ok(exercisesJson);
            }
            catch (Exception ex)
            {
                return Result.Fail<string>($"Failed to retrieve exercisess: {ex.Message}");
            }
        }
    }
}
