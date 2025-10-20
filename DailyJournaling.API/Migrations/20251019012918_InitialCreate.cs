using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyJournaling.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayParts",
                columns: table => new
                {
                    DayPartId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayParts", x => x.DayPartId);
                });

            migrationBuilder.CreateTable(
                name: "MoodStates",
                columns: table => new
                {
                    MoodStateId = table.Column<Guid>(type: "uuid", nullable: false),
                    MoodStateName = table.Column<string>(type: "text", nullable: false),
                    IconUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoodStates", x => x.MoodStateId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    PasswordSalt = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    DayPartId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_DayParts_DayPartId",
                        column: x => x.DayPartId,
                        principalTable: "DayParts",
                        principalColumn: "DayPartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GratitudeRecords",
                columns: table => new
                {
                    GratitudeRecordId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MoodStateId = table.Column<Guid>(type: "uuid", nullable: false),
                    DayPartId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GratitudeRecords", x => x.GratitudeRecordId);
                    table.ForeignKey(
                        name: "FK_GratitudeRecords_DayParts_DayPartId",
                        column: x => x.DayPartId,
                        principalTable: "DayParts",
                        principalColumn: "DayPartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GratitudeRecords_MoodStates_MoodStateId",
                        column: x => x.MoodStateId,
                        principalTable: "MoodStates",
                        principalColumn: "MoodStateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GratitudeRecords_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    GratitudeRecordId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answers_GratitudeRecords_GratitudeRecordId",
                        column: x => x.GratitudeRecordId,
                        principalTable: "GratitudeRecords",
                        principalColumn: "GratitudeRecordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_GratitudeRecordId",
                table: "Answers",
                column: "GratitudeRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_GratitudeRecords_DayPartId",
                table: "GratitudeRecords",
                column: "DayPartId");

            migrationBuilder.CreateIndex(
                name: "IX_GratitudeRecords_MoodStateId",
                table: "GratitudeRecords",
                column: "MoodStateId");

            migrationBuilder.CreateIndex(
                name: "IX_GratitudeRecords_UserId",
                table: "GratitudeRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_DayPartId",
                table: "Questions",
                column: "DayPartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "GratitudeRecords");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "MoodStates");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "DayParts");
        }
    }
}
