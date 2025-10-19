namespace DailyJournaling.API.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string Text { get; set; } = null!;

        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;

        public int GratitudeRecordId { get; set; }
        public GratitudeRecord GratitudeRecord { get; set; } = null!;

    }
}
