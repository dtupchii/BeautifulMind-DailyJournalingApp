using Microsoft.EntityFrameworkCore;
using DailyJournaling.API.Models;

namespace DailyJournaling.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<Answer> Answers { get; set; } = null!;
        public DbSet<MoodState> MoodStates { get; set; } = null!;
        public DbSet<DayPart> DayParts { get; set; } = null!;
        public DbSet<GratitudeRecord> GratitudeRecords { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DayPart>().HasData(
                new DayPart
                {
                    DayPartId = Guid.Parse("ae21d84f-1f1a-4d63-a1cd-edcc41722a6c"),
                    Name = "Morning",
                    StartTime = new TimeSpan(4, 0, 0),
                    EndTime = new TimeSpan(15, 59, 59)
                },
                new DayPart
                {
                    DayPartId = Guid.Parse("1471dab9-6624-4598-aee7-39c73a830d81"),
                    Name = "Evening",
                    StartTime = new TimeSpan(16, 0, 0),
                    EndTime = new TimeSpan(3, 59, 59)
                }
            );

            modelBuilder.Entity<Question>().HasData(
                new Question
                {
                    QuestionId = Guid.Parse("9d8e61ee-6e3a-48ed-9a78-67ce730c403e"),
                    Text = "This morning I am grateful for...",
                    DayPartId = Guid.Parse("ae21d84f-1f1a-4d63-a1cd-edcc41722a6c")
                },
                new Question
                {
                    QuestionId = Guid.Parse("68218811-3cc8-46fe-b957-5682066e8b6c"),
                    Text = "What would make today great?",
                    DayPartId = Guid.Parse("ae21d84f-1f1a-4d63-a1cd-edcc41722a6c")
                },
                new Question
                {
                    QuestionId = Guid.Parse("b6f127ac-d9a6-4337-8eb9-3d54c5a3e4cc"),
                    Text = "Daily affirmation",
                    DayPartId = Guid.Parse("ae21d84f-1f1a-4d63-a1cd-edcc41722a6c")
                },
                new Question
                {
                    QuestionId = Guid.Parse("82c51f06-656c-4261-a941-d6e97cef27fe"),
                    Text = "Highlights of the Day",
                    DayPartId = Guid.Parse("1471dab9-6624-4598-aee7-39c73a830d81")
                },
                new Question
                {
                    QuestionId = Guid.Parse("fd1c739e-20ab-4212-ae9d-ddfa49140e43"),
                    Text = "What did I learn today?",
                    DayPartId = Guid.Parse("1471dab9-6624-4598-aee7-39c73a830d81")
                }
            );

            modelBuilder.Entity<MoodState>().HasData(
                new MoodState
                {
                    MoodStateId = Guid.Parse("8a4381c7-c442-4404-aa78-951edba66a75"),
                    MoodStateName = "Very Happy"
                },
                new MoodState
                {
                    MoodStateId = Guid.Parse("51ad2c99-aaf3-4cca-9456-776e3792d9c9"),
                    MoodStateName = "Happy"
                },
                new MoodState
                {
                    MoodStateId = Guid.Parse("dd074df2-ea0d-4dec-a49a-e788648d98cc"),
                    MoodStateName = "Neutral"
                },
                new MoodState
                {
                    MoodStateId = Guid.Parse("8c26e8d6-1aa5-434e-8391-4079cbc05594"),
                    MoodStateName = "Unhappy"
                },
                new MoodState
                {
                    MoodStateId = Guid.Parse("7b2ba7ce-a5c4-4191-bb11-d73f2cbf80d8"),
                    MoodStateName = "Very Unhappy"
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = Guid.Parse("d290f1ee-6c54-4b01-90e6-d701748f0851"),
                    Email = "admin@mail.com",
                    FirstName = "Admin",
                    LastName = "User",
                    PasswordHash = "hashed_password_placeholder",
                    PasswordSalt = "salt_placeholder",
                    PhoneNumber = "123-456-7890"
                }
            );

            modelBuilder.Entity<GratitudeRecord>().HasData(
                new GratitudeRecord
                {
                    GratitudeRecordId = Guid.Parse("d3b5f3a1-3c4e-4f5a-9f7e-1a2b3c4d5e6f"),
                    CreatedAt = new DateTime(2025, 11, 10, 0, 0, 0, 0, DateTimeKind.Utc),
                    UserId = Guid.Parse("d290f1ee-6c54-4b01-90e6-d701748f0851"),
                    MoodStateId = Guid.Parse("8a4381c7-c442-4404-aa78-951edba66a75"),
                    DayPartId = Guid.Parse("ae21d84f-1f1a-4d63-a1cd-edcc41722a6c")
                }
            );

            modelBuilder.Entity<Answer>().HasData(
                new Answer
                {
                    AnswerId = Guid.Parse("a1b2c3d4-e5f6-4789-0abc-def123456789"),
                    Text = "I am grateful for my family.",
                    QuestionId = Guid.Parse("9d8e61ee-6e3a-48ed-9a78-67ce730c403e"),
                    GratitudeRecordId = Guid.Parse("d3b5f3a1-3c4e-4f5a-9f7e-1a2b3c4d5e6f")
                }
            );
        }
    }
}
