using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class UserChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgeGroup",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ReviewerName" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 33, 23, 258, DateTimeKind.Local).AddTicks(9706), "Anonymous" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "MovieId", "ReviewerName" },
                values: new object[] { new DateTime(2024, 11, 19, 14, 33, 23, 258, DateTimeKind.Local).AddTicks(9715), 4, "johndoe" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 23, 14, 33, 23, 258, DateTimeKind.Local).AddTicks(9721));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 26, 14, 33, 23, 258, DateTimeKind.Local).AddTicks(9726));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 27, 14, 33, 23, 258, DateTimeKind.Local).AddTicks(9731));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "AgeGroup", "Gender", "UserType" },
                values: new object[] { 1, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "AgeGroup", "Gender", "UserType" },
                values: new object[] { 1, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "AgeGroup", "Gender", "UserType" },
                values: new object[] { 1, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                columns: new[] { "AgeGroup", "Gender", "UserType" },
                values: new object[] { 1, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                columns: new[] { "AgeGroup", "Gender", "UserType" },
                values: new object[] { 1, 0, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeGroup",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ReviewerName" },
                values: new object[] { new DateTime(2024, 11, 5, 14, 15, 25, 831, DateTimeKind.Local).AddTicks(6787), "Alice" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "MovieId", "ReviewerName" },
                values: new object[] { new DateTime(2024, 11, 6, 14, 15, 25, 831, DateTimeKind.Local).AddTicks(6796), 1, "Bob" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 10, 14, 15, 25, 831, DateTimeKind.Local).AddTicks(6801));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 13, 14, 15, 25, 831, DateTimeKind.Local).AddTicks(6806));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 14, 15, 25, 831, DateTimeKind.Local).AddTicks(6811));
        }
    }
}
