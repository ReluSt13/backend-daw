using fitness_app_backend.Db;
using FluentResults;

namespace backend_daw.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result<string>> UpdateAvatar(string userId, string avatar)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(userId);

                if (user == null)
                {
                    return Result.Fail("User not found");
                }

                user.Avatar = avatar;

                await _dbContext.SaveChangesAsync();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail($"Failed to update avatar: {ex.Message}");
            }



        }
    }
}
