using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRatingToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 27, 20, 35, 39, 447, DateTimeKind.Local).AddTicks(674));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 28, 20, 35, 39, 447, DateTimeKind.Local).AddTicks(679));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 2, 20, 35, 39, 447, DateTimeKind.Local).AddTicks(682));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 5, 20, 35, 39, 447, DateTimeKind.Local).AddTicks(686));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 6, 20, 35, 39, 447, DateTimeKind.Local).AddTicks(689));
        }
    }
}
