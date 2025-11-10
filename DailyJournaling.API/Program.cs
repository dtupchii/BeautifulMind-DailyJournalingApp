using DailyJournaling.API.Data;
using DailyJournaling.API.Endpoints;
using DailyJournaling.API.Mapping;
using DailyJournaling.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

MapsterConfig.RegisterMappings();

var app = builder.Build();

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

app.Run();
