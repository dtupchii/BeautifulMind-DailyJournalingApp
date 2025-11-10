using DailyJournaling.API.Data;
using Microsoft.EntityFrameworkCore;

namespace DailyJournaling.API.Endpoints
{
    public static class GrartitudeRecordEndpoints
    {
        public static WebApplication MapGratitudeRecordEndpoints(this WebApplication app)
        {
            app.MapGet("api/gratitude-records", async (ApplicationDbContext db) =>
            {
                var records = await db.GratitudeRecords.ToListAsync();
                return Results.Ok(records);
            });

            //app.MapPost("api/gratitude-records/", async (ApplicationDbContext db, GratitudeRecord newRecord) =>
            //{
            //    if (newRecord is null)
            //        return Results.BadRequest("newRecord is null");

            //    // Determine current DayPart based on UTC time
            //    var now = DateTime.UtcNow.Hour;
            //    var dayParts = await db.DayParts.ToListAsync();
            //    var currentDayPart = dayParts.FirstOrDefault(dp =>
            //       now >= dp.StartTime.Hours && now <= dp.EndTime.Hours
            //    );

            //    if (currentDayPart == null)
            //        return Results.BadRequest("No valid DayPart found for the current time.");


            //    newRecord.DayPartId = currentDayPart.DayPartId;
            //    newRecord.GratitudeRecordId = Guid.NewGuid();
            //    newRecord.CreatedAt = DateTime.UtcNow;


            //    db.GratitudeRecords.Add(newRecord);
            //    await db.SaveChangesAsync();

            //    return Results.Created($"/api/gratitude-records/{newRecord.GratitudeRecordId}", newRecord);
            //});

            return app;
        }
    }
}
