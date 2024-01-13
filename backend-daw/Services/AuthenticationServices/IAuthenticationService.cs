using System.IdentityModel.Tokens.Jwt;
using backend_daw.DTOs.Auth;
using FluentResults;

namespace backend_daw.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        Task<Result<string>> Register(RegisterRequest request);
        Task<Result<string>> Login(LoginRequest request);
    }
}
