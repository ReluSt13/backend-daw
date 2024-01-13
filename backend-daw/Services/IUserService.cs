﻿using FluentResults;

namespace backend_daw.Services
{
    public interface IUserService
    {
        Task<Result<string>> UpdateAvatar(string userId, string avatar);
    }
}
