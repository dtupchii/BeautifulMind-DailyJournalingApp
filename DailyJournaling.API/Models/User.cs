using Microsoft.AspNetCore.Identity;

namespace DailyJournaling.API.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public ICollection<GratitudeRecord> GratitudeRecords { get; set; } = new List<GratitudeRecord>();
    }
}
