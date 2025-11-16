using DailyJournaling.API.Models;
using DailyJournaling.API.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DailyJournaling.API.Endpoints
{
    public static class UserLoginEndpoints
    {
        public static RouteGroupBuilder MapUserLoginEndpoints1(this RouteGroupBuilder group)
        {
            group.MapPost("/login", async (UserManager<User> userManager, SignInManager<User> signInManager, LoginRequest request) =>
            {
                var user = await userManager.FindByEmailAsync(request.Email);
                if (user == null)
                    return Results.Unauthorized();

                var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if (!result.Succeeded)
                    return Results.Unauthorized();

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
                //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                return Results.Ok("User is logged-in");
            });
            return group;
        }
    }
}
