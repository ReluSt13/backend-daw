using System.IdentityModel.Tokens.Jwt;
using fitness_app_backend.DTOs;
using FluentResults;

namespace backend_daw.Services
{
    public interface IAuthenticationService
    {
        Task<Result<string>> Register(RegisterRequest request);
        Task<Result<string>> Login(LoginRequest request);
    }
}
