namespace DailyJournaling.API.Models
{
    public class Question
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; } = null!;

        public Guid DayPartId { get; set; }
        public DayPart DayPart { get; set; } = null!;

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
