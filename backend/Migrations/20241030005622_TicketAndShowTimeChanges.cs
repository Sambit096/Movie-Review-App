using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class TicketAndShowTimeChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumOfTickets",
                table: "ShowTimes");

            migrationBuilder.AddColumn<bool>(
                name: "Availability",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Availability",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "NumOfTickets",
                table: "ShowTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
