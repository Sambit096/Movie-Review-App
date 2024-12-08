using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class EnforceUniqueTitleOnMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Movies",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Title",
                table: "Movies",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Movies_Title",
                table: "Movies");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 24, 16, 49, 43, 685, DateTimeKind.Local).AddTicks(5228));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 25, 16, 49, 43, 685, DateTimeKind.Local).AddTicks(5233));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 29, 16, 49, 43, 685, DateTimeKind.Local).AddTicks(5235));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 2, 16, 49, 43, 685, DateTimeKind.Local).AddTicks(5237));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 3, 16, 49, 43, 685, DateTimeKind.Local).AddTicks(5240));
        }
    }
}
