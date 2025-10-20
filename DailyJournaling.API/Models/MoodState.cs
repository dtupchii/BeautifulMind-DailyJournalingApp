namespace DailyJournaling.API.Models
{
    public class MoodState
    {
        public Guid MoodStateId { get; set; }
        public string MoodStateName { get; set; } = null!;
        public string? IconUrl { get; set; }

        public ICollection<GratitudeRecord> GratitudeRecords { get; set; } = new List<GratitudeRecord>();
    }
}
