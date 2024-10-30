using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class AddingInitialMovieData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "Description", "Genre", "Rating", "Title" },
                values: new object[,]
                {
                    { 1, "A group of explorers embarks on a journey to discover ancient civilizations hidden in the Amazon rainforest.", "Adventure", 2, "Future Quest" },
                    { 2, "In a dystopian future, a rogue scientist battles against a corporation that controls the last remaining resources on Earth.", "Sci-Fi", 3, "The Last Horizon" },
                    { 3, "Two astronauts develop an unexpected romance while on a mission to a distant planet.", "Romance", 1, "Love in the Stars" },
                    { 4, "A woman returns to her hometown to confront the memories of her troubled childhood and reconnect with her family.", "Drama", 2, "Echoes of the Past" },
                    { 5, "A former soldier must protect his city from a dangerous gang threatening to take over.", "Action", 3, "Warriors of the Night" },
                    { 6, "A young girl discovers a magical forest where she must battle dark forces to save her world.", "Fantasy", 0, "Mystic Forest" },
                    { 7, "In a world dominated by technology, a hacker uncovers a conspiracy that could endanger humanity.", "Thriller", 2, "Digital Shadows" },
                    { 8, "A team of thieves plans an elaborate heist targeting a high-security bank, but tensions rise as secrets are revealed.", "Crime", 3, "The Great Heist" },
                    { 9, "A marine biologist leads an expedition to explore uncharted underwater caves, discovering hidden treasures and dangers.", "Adventure", 1, "Beneath the Waves" },
                    { 10, "Two strangers meet by chance and find their lives intertwined in unexpected ways, leading to life-altering choices.", "Drama", 2, "Fate's Crossing" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 10);
        }
    }
}
