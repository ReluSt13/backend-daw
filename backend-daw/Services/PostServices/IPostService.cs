using FluentResults;

namespace backend_daw.Services.PostServices
{
    public interface IPostService
    {
        Task<Result<string>> CreatePost(string userId, string content, string image);

        Task<Result<string>> UpdatePost(int postId, string content, string image);

        Task<Result<string>> DeletePost(int postId);

        Task<Result<string>> GetPosts();
    }
}
