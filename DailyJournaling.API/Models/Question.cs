namespace DailyJournaling.API.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Text { get; set; } = null!;

        public int DayPartId { get; set; }
        public DayPart DayPart { get; set; } = null!;

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
