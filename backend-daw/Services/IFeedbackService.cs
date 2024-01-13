using FluentResults;

namespace backend_daw.Services
{
    public interface IFeedbackService
    {
        Task<Result<string>> CreateFeedback(string userId, string name, int postId, bool value);

        Task<Result<string>> DeleteFeedback(string userId, int postId);

        Task<Result<string>> GetFeedbacks();
    }
}