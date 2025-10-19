namespace DailyJournaling.API.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public ICollection<GratitudeRecord> GratitudeRecords { get; set; } = new List<GratitudeRecord>();
    }
}
