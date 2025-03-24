using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketToCode.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixUserColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "username");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Username",
                table: "Users",
                newName: "IX_Users_username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameIndex(
                name: "IX_Users_username",
                table: "Users",
                newName: "IX_Users_Username");
        }
    }
}
