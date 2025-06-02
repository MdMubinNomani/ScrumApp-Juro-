using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumApp__Juro_.Migrations
{
    /// <inheritdoc />
    public partial class UserAuthModelUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserAuths");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserAuths");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserAuths",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserAuths",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
