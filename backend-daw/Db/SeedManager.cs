using backend_daw.Entities;
using fitness_app_backend.Db;
using fitness_app_backend.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend_daw.Db
{
    public class SeedManager
    {
        public static async Task Seed(IServiceProvider services)
        {
            await SeedRoles(services);

            await SeedAdminUser(services);
        }

        private static async Task SeedRoles(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await roleManager.CreateAsync(new IdentityRole(Role.Admin));
            await roleManager.CreateAsync(new IdentityRole(Role.User));
            await roleManager.CreateAsync(new IdentityRole(Role.Verified));
        }

        private static async Task SeedAdminUser(IServiceProvider services)
        {
            var context = services.GetRequiredService<AppDbContext>();
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            var adminUser = await context.Users.FirstOrDefaultAsync(user => user.UserName == "Admin");

            if (adminUser is null)
            {
                adminUser = new User { UserName = "Admin", Email = "admin@email.com" };
                await userManager.CreateAsync(adminUser, "String1!");
                await userManager.AddToRoleAsync(adminUser, Role.Admin);
                await userManager.AddToRoleAsync(adminUser, Role.Verified);
            }
        }
    }
}
