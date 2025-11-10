using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyJournaling.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedTablesWithData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "PhoneNumber" },
                values: new object[] { new Guid("d290f1ee-6c54-4b01-90e6-d701748f0851"), "admin@mail.com", "Admin", "User", "hashed_password_placeholder", "salt_placeholder", "123-456-7890" });

            migrationBuilder.InsertData(
                table: "GratitudeRecords",
                columns: new[] { "GratitudeRecordId", "CreatedAt", "DayPartId", "MoodStateId", "UserId" },
                values: new object[] { new Guid("d3b5f3a1-3c4e-4f5a-9f7e-1a2b3c4d5e6f"), new DateTime(2025, 11, 10, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("ae21d84f-1f1a-4d63-a1cd-edcc41722a6c"), new Guid("8a4381c7-c442-4404-aa78-951edba66a75"), new Guid("d290f1ee-6c54-4b01-90e6-d701748f0851") });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "AnswerId", "GratitudeRecordId", "QuestionId", "Text" },
                values: new object[] { new Guid("a1b2c3d4-e5f6-4789-0abc-def123456789"), new Guid("d3b5f3a1-3c4e-4f5a-9f7e-1a2b3c4d5e6f"), new Guid("9d8e61ee-6e3a-48ed-9a78-67ce730c403e"), "I am grateful for my family." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: new Guid("a1b2c3d4-e5f6-4789-0abc-def123456789"));

            migrationBuilder.DeleteData(
                table: "GratitudeRecords",
                keyColumn: "GratitudeRecordId",
                keyValue: new Guid("d3b5f3a1-3c4e-4f5a-9f7e-1a2b3c4d5e6f"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("d290f1ee-6c54-4b01-90e6-d701748f0851"));
        }
    }
}
