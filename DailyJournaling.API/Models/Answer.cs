namespace DailyJournaling.API.Models
{
    public class Answer
    {
        public Guid AnswerId { get; set; }
        public string Text { get; set; } = null!;

        public Guid QuestionId { get; set; }
        public Question Question { get; set; } = null!;

        public Guid GratitudeRecordId { get; set; }
        public GratitudeRecord GratitudeRecord { get; set; } = null!;

    }
}
