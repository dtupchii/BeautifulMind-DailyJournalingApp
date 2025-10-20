namespace DailyJournaling.API.Models
{
    public class DayPart
    {
        public Guid DayPartId { get; set; }
        public string Name { get; set; } = null!;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<GratitudeRecord> GratitudeRecords { get; set; } = new List<GratitudeRecord>();
    }
}
