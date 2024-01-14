using backend_daw.Entities;
using fitness_app_backend.Db;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace backend_daw.Services.FeedbackServices
{
    public class FeedbackService : IFeedbackService
    {
        private readonly AppDbContext _dbContext;

        public FeedbackService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<string>> CreateFeedback(string userId, string name, int postId, bool value)
        {
            try
            {
                var newFeedback = new Feedback
                {
                    UserId = userId,
                    UserName = name,
                    PostId = postId,
                    Value = value
                };

                _dbContext.Feedbacks.Add(newFeedback);

                await _dbContext.SaveChangesAsync();

                /*var createdLike = await _dbContext.Feedbacks
                    .Include(c => c.User)
                    .Include(c => c.Post)
                    .FirstOrDefaultAsync(c => c.UserId == userId && c.PostId == postId);


                return Result.Ok(JsonConvert.SerializeObject(createdLike));*/

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail<string>($"Failed to like a post: {ex.Message}");
            }

        }

        public async Task<Result<string>> DeleteFeedback(string userId, int postId)
        {
            try
            {
                var feedbackToDelete = await _dbContext.Feedbacks.FindAsync(userId, postId);

                if (feedbackToDelete == null)
                {
                    return Result.Fail<string>("Feedback not found.");
                }

                _dbContext.Feedbacks.Remove(feedbackToDelete);

                await _dbContext.SaveChangesAsync();

                return Result.Ok("Feddback removed.");
            }
            catch (Exception ex)
            {

                return Result.Fail<string>($"Failed to delete the feedback: {ex.Message}");
            }
        }

        public async Task<Result<string>> GetFeedbacks(int postId)
        {
            try
            {
                var allFeedbacks = _dbContext.Feedbacks.Where(f => f.PostId == postId).Include(f => f.User).ToList();

                if (allFeedbacks == null || !allFeedbacks.Any())
                {
                    return Result.Fail<string>("No feedbacks found.");
                }

                var feedbacksJson = JsonConvert.SerializeObject(allFeedbacks);
                return Result.Ok(feedbacksJson);
            }
            catch (Exception ex)
            {
                return Result.Fail<string>($"Failed to retrieve feedbacks: {ex.Message}");
            }
        }
    }
}

