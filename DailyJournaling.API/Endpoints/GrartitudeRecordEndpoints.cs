using DailyJournaling.API.Data;
using DailyJournaling.API.Models;
using DailyJournaling.API.Models.DTOs;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DailyJournaling.API.Endpoints
{
    public static class GrartitudeRecordEndpoints
    {
        public static RouteGroupBuilder MapGratitudeRecordEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", [Authorize] async (ApplicationDbContext db) =>
            {
                var records = await db.GratitudeRecords.Include(u => u.Answers).AsNoTracking().ToListAsync();
                foreach (var rec in records)
                    rec.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(rec.CreatedAt, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
                return Results.Ok(records.Adapt<List<GratitudeRecordDTO>>());
            });
            group.MapGet("/{id}", [Authorize] async (ApplicationDbContext db, Guid id) =>
            {
                var record = await db.GratitudeRecords.Include(u => u.Answers).FirstOrDefaultAsync(r => r.GratitudeRecordId == id);
                if (record is null)
                    return Results.NotFound();
                record.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(record.CreatedAt, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
                return Results.Ok(record.Adapt<GratitudeRecordDTO>());
            });
            group.MapPost("/", [Authorize] async (ApplicationDbContext db, GratitudeRecordCreateUpdateDTO newRecordDTO, HttpContext httpContext) =>
            {
                var moodStatefromDB = await db.MoodStates.FindAsync(newRecordDTO.MoodStateId);
                if (newRecordDTO is null || moodStatefromDB is null)
                    return Results.BadRequest("newRecord is invalid");

                var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return Results.Unauthorized();

                // Determine current DayPart
                // will be changed later to use data from the frontend
                // because dayPay should be determined by the time user started filling out the form
                var now = DateTime.Now.TimeOfDay;
                var dayParts = await db.DayParts.ToListAsync();
                var currentDayPart = dayParts.FirstOrDefault(dp =>
                {
                    if (dp.StartTime <= dp.EndTime)
                    {
                        return now >= dp.StartTime && now <= dp.EndTime;
                    }
                    else
                    {
                        return now >= dp.StartTime || now <= dp.EndTime;
                    }
                });
                if (currentDayPart == null)
                    return Results.BadRequest("No valid DayPart found for the current time.");


                var newRecord = newRecordDTO.Adapt<GratitudeRecord>();
                newRecord.GratitudeRecordId = Guid.NewGuid();
                newRecord.UserId = userId;
                newRecord.CreatedAt = DateTime.UtcNow;
                newRecord.DayPartId = currentDayPart.DayPartId;
                db.GratitudeRecords.Add(newRecord);
                await db.SaveChangesAsync();
                newRecord.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(newRecord.CreatedAt, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));

                return Results.Created($"/{newRecord.GratitudeRecordId}", newRecord.Adapt<GratitudeRecordDTO>());
            });
            group.MapPut("/{id}", [Authorize] async (ApplicationDbContext db, Guid id, GratitudeRecordCreateUpdateDTO updatedRecordDTO) =>
            {
                if (updatedRecordDTO is null)
                    return Results.BadRequest("updatedRecord is null");
                var updatedRecord = updatedRecordDTO.Adapt<GratitudeRecord>();
                var existingRecord = await db.GratitudeRecords.FindAsync(id);
                if (existingRecord is null)
                    return Results.NotFound();
                existingRecord.MoodStateId = updatedRecord.MoodStateId;
                await db.SaveChangesAsync();
                existingRecord.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(existingRecord.CreatedAt, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
                return Results.Ok(existingRecord.Adapt<GratitudeRecordDTO>());
            });
            group.MapDelete("/{id}", [Authorize] async (ApplicationDbContext db, Guid id) =>
            {
                var record = await db.GratitudeRecords.FindAsync(id);
                if (record is null)
                    return Results.NotFound();
                db.GratitudeRecords.Remove(record);
                await db.SaveChangesAsync();
                return Results.Ok("Record deleted");
            });

            return group;
        }
    }
}
