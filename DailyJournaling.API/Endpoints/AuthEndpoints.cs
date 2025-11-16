using DailyJournaling.API.Models;
using DailyJournaling.API.Models.DTOs;
using DailyJournaling.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DailyJournaling.API.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapUserSignUpEndpoints(this WebApplication app)
        {
            app.MapPost("/signup", async (UserManager<User> userManager, SignUpRequest request) =>
            {
                var user = new User
                {
                    UserName = request.Email,
                    Email = request.Email,
                    FirstName = request.Firstname,
                    LastName = request.LastName
                };

                var result = await userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                    return Results.BadRequest(result.Errors);

                await userManager.AddToRoleAsync(user, "User");

                return Results.Ok("User is signed-up");
            });
        }

        public static void MapUserLoginEndpoints(this WebApplication app, IConfiguration config)
        {
            app.MapPost("/login", async (UserManager<User> userManager, LoginRequest request) =>
            {
                var user = await userManager.FindByEmailAsync(request.Email);
                if (user == null)
                    return Results.Unauthorized();

                var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);
                if (!isPasswordValid)
                    return Results.Unauthorized();

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                // Add roles to claims
                var roles = await userManager.GetRolesAsync(user);
                claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:KEY"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: config["JWT:ISSUER"],
                    audience: config["JWT:AUDIENCE"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(double.Parse(config["JWT:DURATION"])),
                    signingCredentials: creds
                    );

                return Results.Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            });
        }
    }
}
