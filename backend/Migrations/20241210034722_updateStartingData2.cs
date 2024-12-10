using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class updateStartingData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 29, 22, 47, 21, 710, DateTimeKind.Local).AddTicks(9130));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 30, 22, 47, 21, 710, DateTimeKind.Local).AddTicks(9142));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 4, 22, 47, 21, 710, DateTimeKind.Local).AddTicks(9146));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 7, 22, 47, 21, 710, DateTimeKind.Local).AddTicks(9150));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 8, 22, 47, 21, 710, DateTimeKind.Local).AddTicks(9153));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "UserType",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 29, 22, 46, 47, 987, DateTimeKind.Local).AddTicks(6762));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 30, 22, 46, 47, 987, DateTimeKind.Local).AddTicks(6770));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 4, 22, 46, 47, 987, DateTimeKind.Local).AddTicks(6775));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 7, 22, 46, 47, 987, DateTimeKind.Local).AddTicks(6781));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 8, 22, 46, 47, 987, DateTimeKind.Local).AddTicks(6847));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "UserType",
                value: 0);
        }
    }
}
