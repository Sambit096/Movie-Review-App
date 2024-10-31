using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class AddUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "email", "firstName", "lastName", "password", "username" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John", "Doe", "d1f23b1a4e5f6a7b8c9d0e", "johndoe" },
                    { 2, "jane.smith@example.com", "Jane", "Smith", "e2f34c1b5g6h7i8j9k0l1m", "janesmith" },
                    { 3, "michael.jones@example.com", "Michael", "Jones", "f3g45d2e6h7i8j9k0l1m2n", "mikejones" },
                    { 4, "sarah.connor@example.com", "Sarah", "Connor", "g4h56e3f7i8j9k0l1m2n3o", "sconnor" },
                    { 5, "david.lee@example.com", "David", "Lee", "h5i67f4g8j9k0l1m2n3o4p", "dlee" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5);
        }
    }
}
