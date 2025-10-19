namespace DailyJournaling.API.Models
{
    public class GratitudeRecord
    {
        public int GratitudeRecordId { get; set; }
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int MoodStateId { get; set; }
        public MoodState MoodState { get; set; } = null!;

        public int DayPartId { get; set; }
        public DayPart DayPart { get; set; } = null!;

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
