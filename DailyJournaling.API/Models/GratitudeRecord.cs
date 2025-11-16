namespace DailyJournaling.API.Models
{
    public class GratitudeRecord
    {
        public Guid GratitudeRecordId { get; set; }
        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid MoodStateId { get; set; }
        public MoodState MoodState { get; set; } = null!;

        public Guid DayPartId { get; set; }
        public DayPart DayPart { get; set; } = null!;

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
