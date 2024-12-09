using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class updateShowtimeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ShowTimes");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 28, 23, 13, 37, 952, DateTimeKind.Local).AddTicks(1727));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 29, 23, 13, 37, 952, DateTimeKind.Local).AddTicks(1741));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 3, 23, 13, 37, 952, DateTimeKind.Local).AddTicks(1747));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 6, 23, 13, 37, 952, DateTimeKind.Local).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 7, 23, 13, 37, 952, DateTimeKind.Local).AddTicks(1760));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ShowTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 28, 9, 15, 52, 988, DateTimeKind.Local).AddTicks(715));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 29, 9, 15, 52, 988, DateTimeKind.Local).AddTicks(720));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 3, 9, 15, 52, 988, DateTimeKind.Local).AddTicks(724));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 6, 9, 15, 52, 988, DateTimeKind.Local).AddTicks(727));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 7, 9, 15, 52, 988, DateTimeKind.Local).AddTicks(730));

            migrationBuilder.UpdateData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 1,
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 2,
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 3,
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 4,
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 5,
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 6,
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 7,
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 8,
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 9,
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 10,
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 11,
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 12,
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 13,
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 14,
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 15,
                column: "Status",
                value: 0);
        }
    }
}
