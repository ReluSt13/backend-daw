using backend_daw.Entities;
using fitness_app_backend.Db;
using fitness_app_backend.Entities;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.Design;

namespace backend_daw.Services.WorkoutExerciseServices
{
    public class WorkoutExerciseService : IWorkoutExerciseService
    {

        private readonly AppDbContext _dbContext;

        public WorkoutExerciseService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result<string>> CreateWorkoutExercise(int workouId, int exerciseId, int reps, int sets, double weight)
        {
            try
            {
                var newWorkoutExercise = new WorkoutExercise
                {
                    WorkoutId = workouId,
                    ExerciseId = exerciseId,
                    Reps =  reps,
                    Sets = sets,
                    Weight = weight
                };

                _dbContext.WorkoutExercises.Add(newWorkoutExercise);

                await _dbContext.SaveChangesAsync();

                var createdWorkout = await _dbContext.WorkoutExercises
                    .Include(we => we.Workout)
                    .Include(we => we.Exercise)
                    .FirstOrDefaultAsync(we => we.Id == newWorkoutExercise.Id);


                return Result.Ok(JsonConvert.SerializeObject(createdWorkout));
            }
            catch (Exception ex)
            {
                return Result.Fail<string>($"Failed to create workout exercise: {ex.Message}");
            }
        }

        public async Task<Result<string>> DeleteWorkoutExercise(int workoutExerciseId)
        {
            try
            {
                var workoutExerciseToDelete = await _dbContext.WorkoutExercises.FindAsync(workoutExerciseId);

                if (workoutExerciseToDelete == null)
                {
                    return Result.Fail<string>("Workout Exercice not found.");
                }

                _dbContext.WorkoutExercises.Remove(workoutExerciseToDelete);

                await _dbContext.SaveChangesAsync();

                return Result.Ok("Workout Exercise removed.");
            }
            catch (Exception ex)
            {

                return Result.Fail<string>($"Failed to delete the wrokout exercise: {ex.Message}");
            }
        }

        public async Task<Result<string>> GetWorkoutExercises(int workoutId)
        {
            try
            {
                var allWorkoutExercises = _dbContext.WorkoutExercises.Where(we => we.WorkoutId == workoutId)
                    .Include(we => we.Exercise).ToList();

                if (allWorkoutExercises == null || !allWorkoutExercises.Any())
                {
                    return Result.Fail<string>("No workout exercises found.");
                }

                var feedbacksJson = JsonConvert.SerializeObject(allWorkoutExercises);
                return Result.Ok(feedbacksJson);
            }
            catch (Exception ex)
            {
                return Result.Fail<string>($"Failed to retrieve workout exercises: {ex.Message}");
            }
        }

        public async Task<Result<string>> UpdateWorkoutExercise(int workoutExerciseId, int? reps, int? sets, double? weight)
        {
            try
            {
                var workoutExerciseToUpdate = await _dbContext.WorkoutExercises.FindAsync(workoutExerciseId);

                if (workoutExerciseToUpdate == null)
                {
                    return Result.Fail<string>("WorkoutExercise not found.");
                }

                if (reps.HasValue && reps.Value > 0)
                {
                    workoutExerciseToUpdate.Reps = reps.Value;
                }

                if (sets.HasValue && sets.Value > 0)
                {
                    workoutExerciseToUpdate.Sets = sets.Value;
                }

                if (weight.HasValue && weight.Value > 0)
                {
                    workoutExerciseToUpdate.Weight = weight.Value;
                }

                await _dbContext.SaveChangesAsync();

                return Result.Ok("WorkoutExercise updated successfully.");
            }
            catch (Exception ex)
            {
                return Result.Fail<string>($"Failed to update the workout exercise: {ex.Message}");
            }
        }
    }
}
