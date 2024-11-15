using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedLikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Likes" },
                values: new object[] { new DateTime(2024, 11, 5, 14, 15, 25, 831, DateTimeKind.Local).AddTicks(6787), 0 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Likes" },
                values: new object[] { new DateTime(2024, 11, 6, 14, 15, 25, 831, DateTimeKind.Local).AddTicks(6796), 0 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Likes" },
                values: new object[] { new DateTime(2024, 11, 10, 14, 15, 25, 831, DateTimeKind.Local).AddTicks(6801), 0 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Likes" },
                values: new object[] { new DateTime(2024, 11, 13, 14, 15, 25, 831, DateTimeKind.Local).AddTicks(6806), 0 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Likes" },
                values: new object[] { new DateTime(2024, 11, 14, 14, 15, 25, 831, DateTimeKind.Local).AddTicks(6811), 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Reviews");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 3, 16, 34, 59, 772, DateTimeKind.Local).AddTicks(3385));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 4, 16, 34, 59, 772, DateTimeKind.Local).AddTicks(3392));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 8, 16, 34, 59, 772, DateTimeKind.Local).AddTicks(3395));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 16, 34, 59, 772, DateTimeKind.Local).AddTicks(3399));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 12, 16, 34, 59, 772, DateTimeKind.Local).AddTicks(3402));
        }
    }
}
