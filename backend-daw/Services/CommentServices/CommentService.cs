using backend_daw.Entities;
using fitness_app_backend.Db;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace backend_daw.Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _dbContext;

        public CommentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result<string>> CreateComment(string userId, string content, string userName, int postId)
        {
            try
            {
                var newComment = new Comment
                {
                    UserId = userId,
                    Content = content,
                    UserName = userName,
                    PostId = postId,
                    DateCreated = DateTime.Now
                };

                _dbContext.Comments.Add(newComment);

                await _dbContext.SaveChangesAsync();

                var createdComment = await _dbContext.Comments
                    .Include(c => c.User)
                    .Include(c => c.Post)
                    .FirstOrDefaultAsync(c => c.UserId == userId && c.PostId == postId);


                return Result.Ok(JsonConvert.SerializeObject(createdComment));
            }
            catch (Exception ex)
            {
                return Result.Fail<string>($"Failed to comment: {ex.Message}");
            }
        }

        public async Task<Result<string>> DeleteComment(int postId, string userId)
        {
            try
            {
                var commentToDelete = await _dbContext.Comments.FindAsync(userId, postId);

                if (commentToDelete == null)
                {
                    return Result.Fail<string>("Feedback not found.");
                }

                _dbContext.Comments.Remove(commentToDelete);

                await _dbContext.SaveChangesAsync();

                return Result.Ok("Comment removed.");
            }
            catch (Exception ex)
            {

                return Result.Fail<string>($"Failed to delete the comment: {ex.Message}");
            }
        }

        public async Task<Result<string>> GetComments(int postId)
        {
            try
            {
                var allComments = _dbContext.Comments.Where(c => c.PostId == postId).Include(c => c.User).ToList();

                if (allComments == null || !allComments.Any())
                {
                    return Result.Fail<string>("No comments found.");
                }

                var feedbacksJson = JsonConvert.SerializeObject(allComments);
                return Result.Ok(feedbacksJson);
            }
            catch (Exception ex)
            {
                return Result.Fail<string>($"Failed to retrieve comments: {ex.Message}");
            }
        }

        public async Task<Result<string>> UpdateComment(string userId, int postId, string content)
        {
            try
            {
                var commentToUpdate = await _dbContext.Comments.FindAsync(userId, postId);

                if (commentToUpdate == null)
                {
                    return Result.Fail<string>("Comment not found.");
                }

                if (content != null)
                {
                    commentToUpdate.Content = content;
                }

                await _dbContext.SaveChangesAsync();

                return Result.Ok("Comment updated successfully.");
            }
            catch (Exception ex)
            {
                return Result.Fail<string>($"Failed to update the comment: {ex.Message}");
            }
        }
    }
}
