using Microsoft.AspNetCore.Identity;
using DailyJournaling.API.Models;

namespace DailyJournaling.API.Data
{
    public static class IdentitySeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider services, IConfiguration config)
        {
            using var scope = services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new IdentityRole("User"));

            var adminUser = await userManager.FindByEmailAsync("admin@mail.com");
            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = "admin@mail.com",
                    Email = "admin@mail.com",
                    FirstName = "Admin",
                    LastName = "User"
                };

                var result = await userManager.CreateAsync(adminUser, config["AdminUser:Password"]);
                if (!result.Succeeded)
                    throw new Exception("Failed to create admin user" + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            //var adminUser = await userManager.FindByEmailAsync("admin@mail.com");
            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
