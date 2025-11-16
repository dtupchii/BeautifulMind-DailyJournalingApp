using DailyJournaling.API.Models;
using DailyJournaling.API.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace DailyJournaling.API.Endpoints
{
    public static class UserSignUpEndpoints
    {
        public static RouteGroupBuilder MapUserSignUpEndpoints1(this RouteGroupBuilder group)
        {
            group.MapPost("/signup", async (UserManager<User> userManager, SignUpRequest request) =>
            {
                // Placeholder for user sign-up logic
                var user = new User
                {
                    UserName = request.Email,
                    Email = request.Email
                };

                var result = await userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                    return Results.BadRequest(result.Errors);

                return Results.Ok("User is signed-up");
            });

            return group;
        }
    }
}
