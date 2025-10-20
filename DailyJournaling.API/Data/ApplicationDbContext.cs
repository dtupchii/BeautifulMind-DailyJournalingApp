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
    }
}
