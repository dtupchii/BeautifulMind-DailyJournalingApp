namespace DailyJournaling.API.Models.DTOs
{
    public class AnswerDTO
    {
        public Guid AnswerId { get; set; }
        public string Text { get; set; } = null!;
    }
}
