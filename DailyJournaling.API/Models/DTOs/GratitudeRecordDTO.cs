namespace DailyJournaling.API.Models.DTOs
{
    public class GratitudeRecordDTO
    {
        public Guid GratitudeRecordId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public Guid MoodStateId { get; set; }
        public Guid DayPartId { get; set; }
        public ICollection<AnswerDTO>? Answers { get; set; }
    }
}
