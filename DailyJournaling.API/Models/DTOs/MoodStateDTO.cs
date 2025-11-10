namespace DailyJournaling.API.Models.DTOs
{
    public class MoodStateDTO
    {
        public Guid MoodStateId { get; set; }
        public string MoodStateName { get; set; } = null!;
        public string? IconUrl { get; set; }
    }
}
