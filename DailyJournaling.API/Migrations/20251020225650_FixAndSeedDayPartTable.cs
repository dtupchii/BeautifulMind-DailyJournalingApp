using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyJournaling.API.Migrations
{
    /// <inheritdoc />
    public partial class FixAndSeedDayPartTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "DayParts",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "DayParts",
                keyColumn: "DayPartId",
                keyValue: new Guid("1471dab9-6624-4598-aee7-39c73a830d81"),
                column: "EndTime",
                value: new TimeSpan(0, 3, 59, 59, 0));

            migrationBuilder.UpdateData(
                table: "DayParts",
                keyColumn: "DayPartId",
                keyValue: new Guid("ae21d84f-1f1a-4d63-a1cd-edcc41722a6c"),
                column: "EndTime",
                value: new TimeSpan(0, 15, 59, 59, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "DayParts");
        }
    }
}
