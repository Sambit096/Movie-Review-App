using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class AddDummyReviewData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ReviewerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "Content", "CreatedAt", "MovieId", "ReviewerName" },
                values: new object[,]
                {
                    { 1, "An incredible journey through dreams.", new DateTime(2024, 11, 3, 12, 24, 32, 340, DateTimeKind.Local).AddTicks(85), 1, "Alice" },
                    { 2, "A masterpiece in modern cinema.", new DateTime(2024, 11, 4, 12, 24, 32, 340, DateTimeKind.Local).AddTicks(91), 1, "Bob" },
                    { 3, "Great for kids and adults alike.", new DateTime(2024, 11, 8, 12, 24, 32, 340, DateTimeKind.Local).AddTicks(94), 2, "Charlie" },
                    { 4, "A compelling story about loyalty and power.", new DateTime(2024, 11, 11, 12, 24, 32, 340, DateTimeKind.Local).AddTicks(97), 3, "Dave" },
                    { 5, "One of the best films ever made.", new DateTime(2024, 11, 12, 12, 24, 32, 340, DateTimeKind.Local).AddTicks(100), 3, "Eve" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MovieId",
                table: "Reviews",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
