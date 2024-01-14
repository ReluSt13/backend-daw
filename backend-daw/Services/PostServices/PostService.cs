using backend_daw.Entities;
using fitness_app_backend.Db;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace backend_daw.Services.PostServices
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _dbContext;

        public PostService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<string>> CreatePost(string userId, string content, string image)
        {
            try
            {
                var newPost = new Post
                {
                    UserId = userId,
                    Content = content,
                    Image = image,
                    DateAdded = DateTime.Now
                };

                _dbContext.Posts.Add(newPost);

                await _dbContext.SaveChangesAsync();

                var createdPost = await _dbContext.Posts
                    .Include(p => p.User)
                    .FirstOrDefaultAsync(p => p.Id == newPost.Id);

                return Result.Ok(JsonConvert.SerializeObject(createdPost));
            }
            catch (Exception ex)
            {
                return Result.Fail<string>($"Failed to create the post: {ex.Message}");
            }

        }

        public async Task<Result<string>> DeletePost(int postId)
        {
            try
            {
                var postToDelete = await _dbContext.Posts.FindAsync(postId);

                if (postToDelete == null)
                {
                    return Result.Fail<string>("Post not found.");
                }

                _dbContext.Posts.Remove(postToDelete);

                await _dbContext.SaveChangesAsync();

                return Result.Ok("Post deleted successfully.");
            }
            catch (Exception ex)
            {

                return Result.Fail<string>($"Failed to delete the post: {ex.Message}");
            }
        }

        public async Task<Result<string>> GetPosts()
        {
            try 
            {
                var allPosts = _dbContext.Posts
                    .Include(p => p.User)
                    .Include(p => p.Feedbacks)
                    .Include(p => p.Comments)
                    .OrderByDescending(p => p.DateAdded)
                    .ToList();

                if (allPosts == null || !allPosts.Any())
                {
                    return Result.Fail<string>("No posts found.");
                }

                var postsJson = JsonConvert.SerializeObject(allPosts);
                return Result.Ok(postsJson);
            }
            catch (Exception ex)
            {
                return Result.Fail<string>($"Failed to retrieve posts: {ex.Message}");
            }
        }

        public async Task<Result<string>> UpdatePost(int postId, string content, string image)
        {
            try
            {
                var postToUpdate = await _dbContext.Posts.FindAsync(postId);

                if (postToUpdate == null)
                {
                    return Result.Fail<string>("Post not found.");
                }

                if (content != null)
                {
                    postToUpdate.Content = content;
                }

                if (image != null)
                {
                    postToUpdate.Image = image;
                }

                await _dbContext.SaveChangesAsync();

                return Result.Ok("Post updated successfully.");
            }
            catch (Exception ex)
            {
                return Result.Fail<string>($"Failed to update the post: {ex.Message}");
            }

        }
    }
}
