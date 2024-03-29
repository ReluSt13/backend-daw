﻿using FluentResults;

namespace backend_daw.Services.CommentServices
{
    public interface ICommentService
    {
        Task<Result<string>> CreateComment(string userId, string content, string userName, int postId);

        Task<Result<string>> UpdateComment(int commentId, string content);

        Task<Result<string>> DeleteComment(int commentId);

        Task<Result<string>> GetComments(int postId);
    }
}
