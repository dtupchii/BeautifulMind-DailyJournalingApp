namespace DailyJournaling.API.Models.DTOs
{
    public class MoodStateCreateUpdateDTO
    {
        public string MoodStateName { get; set; } = null!;
        public string? IconUrl { get; set; }
    }
}
