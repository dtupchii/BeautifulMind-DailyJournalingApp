namespace DailyJournaling.API.Models.DTOs;
public record SignUpRequest(string Email, string Password, string Firstname, string LastName);