using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class AddingMoreInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 1,
                columns: new[] { "Description", "Genre", "Rating", "Title" },
                values: new object[] { "John Wick uncovers a path to defeating The High Table, but before he can earn his freedom, he must face a new enemy with powerful alliances across the globe.", "Action", 3, "John Wick: Chapter 4" });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 2,
                columns: new[] { "Description", "Genre", "Rating", "Title" },
                values: new object[] { "After being expelled from Barbieland for being a less-than-perfect doll, Barbie embarks on a journey of self-discovery in the real world.", "Comedy", 1, "Barbie" });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 3,
                columns: new[] { "Description", "Genre", "Rating", "Title" },
                values: new object[] { "A dramatic portrayal of the life of J. Robert Oppenheimer and his role in the development of the atomic bomb.", "Biography, Drama, History", 3, "Oppenheimer" });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 4,
                columns: new[] { "Description", "Genre", "Title" },
                values: new object[] { "The Guardians must protect Rocket, who is in grave danger, while dealing with their pasts and new threats to the galaxy.", "Action, Adventure, Comedy", "Guardians of the Galaxy Vol. 3" });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 5,
                columns: new[] { "Description", "Genre", "Rating", "Title" },
                values: new object[] { "Ethan Hunt and his IMF team must track down a dangerous new weapon before it falls into the wrong hands.", "Action, Adventure, Thriller", 2, "Mission: Impossible - Dead Reckoning Part One" });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 6,
                columns: new[] { "Description", "Genre", "Rating", "Title" },
                values: new object[] { "Miles Morales returns for a new adventure across the multiverse, meeting other Spider-People along the way.", "Animation, Action, Adventure", 1, "Spider-Man: Across the Spider-Verse" });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 7,
                columns: new[] { "Description", "Genre", "Rating", "Title" },
                values: new object[] { "Captain Marvel teams up with Ms. Marvel and Monica Rambeau to face a new cosmic threat.", "Action, Adventure, Fantasy", 1, "The Marvels" });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 8,
                columns: new[] { "Description", "Genre", "Rating", "Title" },
                values: new object[] { "Mario and Luigi embark on a journey through the Mushroom Kingdom to rescue Princess Peach from Bowser.", "Animation, Adventure, Comedy", 1, "The Super Mario Bros. Movie" });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 9,
                columns: new[] { "Description", "Genre", "Rating", "Title" },
                values: new object[] { "Two estranged sisters must battle a group of flesh-possessing demons to save the family they never knew they had.", "Horror", 3, "Evil Dead Rise" });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 10,
                columns: new[] { "Description", "Genre", "Title" },
                values: new object[] { "Paul Atreides unites with Chani and the Fremen while seeking revenge against those who destroyed his family.", "Action, Adventure, Sci-Fi", "Dune: Part Two" });

            migrationBuilder.InsertData(
                table: "ShowTimes",
                columns: new[] { "ShowTimeId", "MovieId", "Status", "ViewingTime" },
                values: new object[,]
                {
                    { 1, 1, 0, new DateTime(2024, 10, 29, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, 0, new DateTime(2024, 11, 1, 18, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, 0, new DateTime(2024, 11, 2, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 3, 0, new DateTime(2024, 10, 30, 16, 15, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 3, 0, new DateTime(2024, 11, 4, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 4, 0, new DateTime(2024, 11, 6, 11, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 5, 0, new DateTime(2024, 11, 12, 17, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 5, 0, new DateTime(2024, 11, 14, 12, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 6, 0, new DateTime(2024, 11, 1, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 7, 0, new DateTime(2024, 11, 5, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 8, 0, new DateTime(2024, 10, 31, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 8, 0, new DateTime(2024, 11, 3, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 9, 0, new DateTime(2024, 11, 8, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 10, 0, new DateTime(2024, 11, 10, 16, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 15, 10, 0, new DateTime(2024, 11, 19, 18, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "Availability", "CartId", "Price", "Quantity", "ShowTimeId" },
                values: new object[,]
                {
                    { 1, true, null, 12.5, 1, 1 },
                    { 2, false, null, 12.5, 1, 1 },
                    { 3, true, null, 14.0, 1, 2 },
                    { 4, false, null, 14.0, 1, 2 },
                    { 5, false, null, 14.0, 1, 2 },
                    { 6, true, null, 10.0, 1, 3 },
                    { 7, true, null, 15.0, 1, 4 },
                    { 8, false, null, 15.0, 1, 4 },
                    { 9, false, null, 15.0, 1, 4 },
                    { 10, true, null, 11.0, 1, 5 },
                    { 11, false, null, 11.0, 1, 5 },
                    { 12, true, null, 13.5, 1, 6 },
                    { 13, false, null, 13.5, 1, 6 },
                    { 14, false, null, 13.5, 1, 6 },
                    { 15, true, null, 12.0, 1, 7 },
                    { 16, true, null, 15.5, 1, 8 },
                    { 17, false, null, 15.5, 1, 8 },
                    { 18, true, null, 13.0, 1, 9 },
                    { 19, false, null, 13.0, 1, 9 },
                    { 20, true, null, 16.0, 1, 10 },
                    { 21, false, null, 16.0, 1, 10 },
                    { 22, true, null, 14.5, 1, 11 },
                    { 23, false, null, 14.5, 1, 11 },
                    { 24, true, null, 10.5, 1, 12 },
                    { 25, false, null, 10.5, 1, 12 },
                    { 26, true, null, 11.0, 1, 13 },
                    { 27, true, null, 12.0, 1, 14 },
                    { 28, false, null, 12.0, 1, 14 },
                    { 29, false, null, 12.0, 1, 14 },
                    { 30, true, null, 13.0, 1, 15 },
                    { 31, false, null, 13.0, 1, 15 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ShowTimes",
                keyColumn: "ShowTimeId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: 31);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 1,
                columns: new[] { "Description", "Genre", "Rating", "Title" },
                values: new object[] { "A group of explorers embarks on a journey to discover ancient civilizations hidden in the Amazon rainforest.", "Adventure", 2, "Future Quest" });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 2,
                columns: new[] { "Description", "Genre", "Rating", "Title" },
                values: new object[] { "In a dystopian future, a rogue scientist battles against a corporation that controls the last remaining resources on Earth.", "Sci-Fi", 3, "The Last Horizon" });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 3,
                columns: new[] { "Description", "Genre", "Rating", "Title" },
                values: new object[] { "Two astronauts develop an unexpected romance while on a mission to a distant planet.", "Romance", 1, "Love in the Stars" });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 4,
                columns: new[] { "Description", "Genre", "Title" },
                values: new object[] { "A woman returns to her hometown to confront the memories of her troubled childhood and reconnect with her family.", "Drama", "Echoes of the Past" });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 5,
                columns: new[] { "Description", "Genre", "Rating", "Title" },
                values: new object[] { "A former soldier must protect his city from a dangerous gang threatening to take over.", "Action", 3, "Warriors of the Night" });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 6,
                columns: new[] { "Description", "Genre", "Rating", "Title" },
                values: new object[] { "A young girl discovers a magical forest where she must battle dark forces to save her world.", "Fantasy", 0, "Mystic Forest" });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 7,
                columns: new[] { "Description", "Genre", "Rating", "Title" },
                values: new object[] { "In a world dominated by technology, a hacker uncovers a conspiracy that could endanger humanity.", "Thriller", 2, "Digital Shadows" });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 8,
                columns: new[] { "Description", "Genre", "Rating", "Title" },
                values: new object[] { "A team of thieves plans an elaborate heist targeting a high-security bank, but tensions rise as secrets are revealed.", "Crime", 3, "The Great Heist" });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 9,
                columns: new[] { "Description", "Genre", "Rating", "Title" },
                values: new object[] { "A marine biologist leads an expedition to explore uncharted underwater caves, discovering hidden treasures and dangers.", "Adventure", 1, "Beneath the Waves" });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 10,
                columns: new[] { "Description", "Genre", "Title" },
                values: new object[] { "Two strangers meet by chance and find their lives intertwined in unexpected ways, leading to life-altering choices.", "Drama", "Fate's Crossing" });
        }
    }
}
