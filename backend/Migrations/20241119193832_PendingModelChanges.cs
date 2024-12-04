using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class PendingModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 9, 14, 38, 31, 541, DateTimeKind.Local).AddTicks(9120));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 10, 14, 38, 31, 541, DateTimeKind.Local).AddTicks(9620));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 14, 38, 31, 541, DateTimeKind.Local).AddTicks(9620));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 17, 14, 38, 31, 541, DateTimeKind.Local).AddTicks(9630));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 18, 14, 38, 31, 541, DateTimeKind.Local).AddTicks(9630));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 9, 14, 36, 59, 894, DateTimeKind.Local).AddTicks(7260));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 10, 14, 36, 59, 894, DateTimeKind.Local).AddTicks(7810));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 14, 36, 59, 894, DateTimeKind.Local).AddTicks(7820));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 17, 14, 36, 59, 894, DateTimeKind.Local).AddTicks(7820));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 18, 14, 36, 59, 894, DateTimeKind.Local).AddTicks(7830));
        }
    }
}
