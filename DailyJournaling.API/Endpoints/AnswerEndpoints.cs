using DailyJournaling.API.Data;
using DailyJournaling.API.Models;
using DailyJournaling.API.Models.DTOs;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DailyJournaling.API.Endpoints
{
    public static class AnswerEndpoints
    {
        public static RouteGroupBuilder MapAnswerEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (ApplicationDbContext db) =>
            {
                var answers = await db.Answers.ToListAsync();
                return Results.Ok(answers.Adapt<List<AnswerDTO>>());
            });
            group.MapGet("/{id}", async (ApplicationDbContext db, Guid id) =>
            {
                var answer = await db.Answers.FindAsync(id);
                return answer is not null ? Results.Ok(answer.Adapt<AnswerDTO>()) : Results.NotFound();
            });
            group.MapPost("/", async (ApplicationDbContext db, AnswerCreateUpdateDTO newAnswerCreateUpdateDTO) =>
            {
                if (newAnswerCreateUpdateDTO is null)
                    return Results.BadRequest("newAnswerCreateUpdateDTO is null");
                var newAnswer = newAnswerCreateUpdateDTO.Adapt<Answer>();
                newAnswer.AnswerId = Guid.NewGuid();
                newAnswer.GratitudeRecordId = Guid.Parse("d3b5f3a1-3c4e-4f5a-9f7e-1a2b3c4d5e6f"); // will change later to use data from the frontend
                newAnswer.QuestionId = Guid.Parse("9d8e61ee-6e3a-48ed-9a78-67ce730c403e"); // will change later to use data from the frontend
                db.Answers.Add(newAnswer);
                await db.SaveChangesAsync();
                return Results.Created($"/api/answers/{newAnswer.AnswerId}", newAnswer.Adapt<AnswerDTO>());
            });
            group.MapPut("/{id}", async (ApplicationDbContext db, Guid id, AnswerCreateUpdateDTO updatedAnswerDTO) =>
            {
                if (updatedAnswerDTO is null)
                    return Results.BadRequest("updatedAnswer is null");
                var updatedAnswer = updatedAnswerDTO.Adapt<Answer>();
                var existingAnswer = await db.Answers.FindAsync(id);
                if (existingAnswer is null)
                    return Results.NotFound();
                existingAnswer.Text = updatedAnswer.Text;
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            group.MapDelete("/{id}", async (ApplicationDbContext db, Guid id) =>
            {
                var answer = await db.Answers.FindAsync(id);
                if (answer is null)
                    return Results.NotFound();
                db.Answers.Remove(answer);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            return group;
        }
    }
}
