using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DailyJournaling.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DayParts",
                columns: new[] { "DayPartId", "Name", "StartTime" },
                values: new object[,]
                {
                    { new Guid("1471dab9-6624-4598-aee7-39c73a830d81"), "Evening", new TimeSpan(0, 16, 0, 0, 0) },
                    { new Guid("ae21d84f-1f1a-4d63-a1cd-edcc41722a6c"), "Morning", new TimeSpan(0, 4, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "MoodStates",
                columns: new[] { "MoodStateId", "IconUrl", "MoodStateName" },
                values: new object[,]
                {
                    { new Guid("51ad2c99-aaf3-4cca-9456-776e3792d9c9"), null, "Happy" },
                    { new Guid("7b2ba7ce-a5c4-4191-bb11-d73f2cbf80d8"), null, "Very Unhappy" },
                    { new Guid("8a4381c7-c442-4404-aa78-951edba66a75"), null, "Very Happy" },
                    { new Guid("8c26e8d6-1aa5-434e-8391-4079cbc05594"), null, "Unhappy" },
                    { new Guid("dd074df2-ea0d-4dec-a49a-e788648d98cc"), null, "Neutral" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "DayPartId", "Text" },
                values: new object[,]
                {
                    { new Guid("68218811-3cc8-46fe-b957-5682066e8b6c"), new Guid("ae21d84f-1f1a-4d63-a1cd-edcc41722a6c"), "What would make today great?" },
                    { new Guid("82c51f06-656c-4261-a941-d6e97cef27fe"), new Guid("1471dab9-6624-4598-aee7-39c73a830d81"), "Highlights of the Day" },
                    { new Guid("9d8e61ee-6e3a-48ed-9a78-67ce730c403e"), new Guid("ae21d84f-1f1a-4d63-a1cd-edcc41722a6c"), "This morning I am grateful for..." },
                    { new Guid("b6f127ac-d9a6-4337-8eb9-3d54c5a3e4cc"), new Guid("ae21d84f-1f1a-4d63-a1cd-edcc41722a6c"), "Daily affirmation" },
                    { new Guid("fd1c739e-20ab-4212-ae9d-ddfa49140e43"), new Guid("1471dab9-6624-4598-aee7-39c73a830d81"), "What did I learn today?" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MoodStates",
                keyColumn: "MoodStateId",
                keyValue: new Guid("51ad2c99-aaf3-4cca-9456-776e3792d9c9"));

            migrationBuilder.DeleteData(
                table: "MoodStates",
                keyColumn: "MoodStateId",
                keyValue: new Guid("7b2ba7ce-a5c4-4191-bb11-d73f2cbf80d8"));

            migrationBuilder.DeleteData(
                table: "MoodStates",
                keyColumn: "MoodStateId",
                keyValue: new Guid("8a4381c7-c442-4404-aa78-951edba66a75"));

            migrationBuilder.DeleteData(
                table: "MoodStates",
                keyColumn: "MoodStateId",
                keyValue: new Guid("8c26e8d6-1aa5-434e-8391-4079cbc05594"));

            migrationBuilder.DeleteData(
                table: "MoodStates",
                keyColumn: "MoodStateId",
                keyValue: new Guid("dd074df2-ea0d-4dec-a49a-e788648d98cc"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: new Guid("68218811-3cc8-46fe-b957-5682066e8b6c"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: new Guid("82c51f06-656c-4261-a941-d6e97cef27fe"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: new Guid("9d8e61ee-6e3a-48ed-9a78-67ce730c403e"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: new Guid("b6f127ac-d9a6-4337-8eb9-3d54c5a3e4cc"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: new Guid("fd1c739e-20ab-4212-ae9d-ddfa49140e43"));

            migrationBuilder.DeleteData(
                table: "DayParts",
                keyColumn: "DayPartId",
                keyValue: new Guid("1471dab9-6624-4598-aee7-39c73a830d81"));

            migrationBuilder.DeleteData(
                table: "DayParts",
                keyColumn: "DayPartId",
                keyValue: new Guid("ae21d84f-1f1a-4d63-a1cd-edcc41722a6c"));
        }
    }
}
