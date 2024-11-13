using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class Ratings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Rating" },
                values: new object[] { new DateTime(2024, 11, 3, 16, 11, 8, 491, DateTimeKind.Local).AddTicks(8776), 5 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Rating" },
                values: new object[] { new DateTime(2024, 11, 4, 16, 11, 8, 491, DateTimeKind.Local).AddTicks(8783), 5 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Rating" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 11, 8, 491, DateTimeKind.Local).AddTicks(8787), 2 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Rating" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 11, 8, 491, DateTimeKind.Local).AddTicks(8792), 3 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Rating" },
                values: new object[] { new DateTime(2024, 11, 12, 16, 11, 8, 491, DateTimeKind.Local).AddTicks(8795), 5 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Reviews");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 3, 12, 24, 32, 340, DateTimeKind.Local).AddTicks(85));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 4, 12, 24, 32, 340, DateTimeKind.Local).AddTicks(91));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 8, 12, 24, 32, 340, DateTimeKind.Local).AddTicks(94));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 24, 32, 340, DateTimeKind.Local).AddTicks(97));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 12, 12, 24, 32, 340, DateTimeKind.Local).AddTicks(100));
        }
    }
}
