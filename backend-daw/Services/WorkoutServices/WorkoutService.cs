using backend_daw.Entities;
using fitness_app_backend.Db;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace backend_daw.Services.WorkoutServices
{
    public class WorkoutService : IWorkoutService
    {
        private readonly AppDbContext _dbContext;

        public WorkoutService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<string>> CreateWorkout(string userId, string name)
        {
            try
            {
                var newWorkout = new Workout
                {
                    UserId = userId,
                    Name = name,
                    DateCreated = DateTime.Now,
                };

                _dbContext.Workouts.Add(newWorkout);

                await _dbContext.SaveChangesAsync();

                var createdWorkout = await _dbContext.Workouts
                    .Include(we => we.User)
                    .FirstOrDefaultAsync(we => we.Id == newWorkout.Id);

                return Result.Ok(JsonConvert.SerializeObject(createdWorkout));
            }
            catch (Exception ex)
            {
                return Result.Fail<string>($"Failed to create the workout: {ex.Message}");
            }

        }

        public async Task<Result<string>> DeleteWorkout(int workoutId)
        {
            try
            {
                var workoutToDelete = await _dbContext.Workouts.FindAsync(workoutId);

                if (workoutToDelete == null)
                {
                    return Result.Fail<string>("Workout not found.");
                }

                _dbContext.Workouts.Remove(workoutToDelete);

                await _dbContext.SaveChangesAsync();

                return Result.Ok("Workout deleted successfully.");
            }
            catch (Exception ex)
            {

                return Result.Fail<string>($"Failed to delete the workout: {ex.Message}");
            }
        }

        public async Task<Result<string>> GetWorkouts(string userId)
        {
            try
            {
                var allWorkouts = _dbContext.Workouts.Where(w => w.UserId == userId).Include(c => c.User).ToList();

                if (allWorkouts == null || !allWorkouts.Any())
                {
                    return Result.Fail<string>("No workouts found.");
                }

                var feedbacksJson = JsonConvert.SerializeObject(allWorkouts);
                return Result.Ok(feedbacksJson);
            }
            catch (Exception ex)
            {
                return Result.Fail<string>($"Failed to retrieve workoutss: {ex.Message}");
            }
        }

        public async Task<Result<string>> UpdateWorkout(int workoutId, string name)
        {
            try
            {
                var workoutToUpdate = await _dbContext.Workouts.FindAsync(workoutId);

                if (workoutToUpdate == null)
                {
                    return Result.Fail<string>("Workout not found.");
                }

                if (name != null)
                {
                    workoutToUpdate.Name = name;
                }

                await _dbContext.SaveChangesAsync();

                return Result.Ok("Workout updated successfully.");
            }
            catch (Exception ex)
            {
                return Result.Fail<string>($"Failed to update the workout: {ex.Message}");
            }
        }
    }

}

