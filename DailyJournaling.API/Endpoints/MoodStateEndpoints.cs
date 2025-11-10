using DailyJournaling.API.Data;
using DailyJournaling.API.Models;
using DailyJournaling.API.Models.DTOs;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DailyJournaling.API.Endpoints
{
    public static class MoodStateEndpoints
    {
        public static RouteGroupBuilder MapMoodStateEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (ApplicationDbContext db) =>
            {
                var moodStates = await db.MoodStates.ToListAsync();
                return Results.Ok(moodStates.Adapt<List<MoodStateDTO>>());
            });
            group.MapGet("/{id}", async (ApplicationDbContext db, Guid id) =>
            {
                var moodState = await db.MoodStates.FindAsync(id);
                return moodState is not null ? Results.Ok(moodState.Adapt<MoodStateDTO>()) : Results.NotFound();
            });
            group.MapPost("/", async (ApplicationDbContext db, MoodStateCreateUpdateDTO newMoodStateCreateUpdateDTO) =>
            {
                if (newMoodStateCreateUpdateDTO is null)
                    return Results.BadRequest("newMoodStateCreateUpdateDTO is null");
                var newMoodState = newMoodStateCreateUpdateDTO.Adapt<MoodState>();
                newMoodState.MoodStateId = Guid.NewGuid();
                db.MoodStates.Add(newMoodState);
                await db.SaveChangesAsync();
                return Results.Created($"/api/moodstates/{newMoodState.MoodStateId}", newMoodState.Adapt<MoodStateDTO>());
            });
            group.MapPut("/{id}", async (ApplicationDbContext db, Guid id, MoodStateCreateUpdateDTO updatedMoodStateDTO) =>
            {
                if (updatedMoodStateDTO is null)
                    return Results.BadRequest("updatedMoodState is null");
                var updatedMoodState = updatedMoodStateDTO.Adapt<MoodState>();
                var existingMoodState = await db.MoodStates.FindAsync(id);
                if (existingMoodState is null)
                    return Results.NotFound();
                existingMoodState.MoodStateName = updatedMoodState.MoodStateName;
                existingMoodState.IconUrl = updatedMoodState.IconUrl;
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            group.MapDelete("/{id}", async (ApplicationDbContext db, Guid id) =>
            {
                var moodState = await db.MoodStates.FindAsync(id);
                if (moodState is null)
                    return Results.NotFound();
                db.MoodStates.Remove(moodState);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });


            return group;
        }
    }
}
