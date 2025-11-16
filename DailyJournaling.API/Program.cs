using DailyJournaling.API.Data;
using DailyJournaling.API.Endpoints;
using DailyJournaling.API.Mapping;
using DailyJournaling.API.Models;
using DailyJournaling.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var jwtKey = builder.Configuration["JWT:KEY"];
var jwtIssuer = builder.Configuration["JWT:ISSUER"];
var jwtAudience = builder.Configuration["JWT:AUDIENCE"];
var jwtDuration = int.Parse(builder.Configuration["JWT:DURATION"] ?? "60");

builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection") ?? 
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});
builder.Services.AddAuthorization();
builder.Services.AddSwaggerWithJwt();

MapsterConfig.RegisterMappings();

var app = builder.Build();

await IdentitySeeder.SeedRolesAndAdminAsync(app.Services, builder.Configuration);
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    var appliedMigrations = db.Database.GetAppliedMigrations();
    foreach (var m in appliedMigrations)
        Console.WriteLine(m);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/openapi");
}
app.UseHttpsRedirection();

app.MapGet("/", () => Results.Ok("API is running")).WithOpenApi();

app.MapGroup("/api/moodstates").MapMoodStateEndpoints();
app.MapGroup("/api/answers").MapAnswerEndpoints();
app.MapGroup("/api/gratitude-records").MapGratitudeRecordEndpoints();
app.MapUserSignUpEndpoints();
app.MapUserLoginEndpoints(builder.Configuration);
// Protect test endpoint with role
app.MapGet("/admin-data", [Authorize(Roles = "Admin")] () => "Secret admin data");



app.Run();
