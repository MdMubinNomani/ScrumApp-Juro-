using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumApp__Juro_.Migrations
{
    /// <inheritdoc />
    public partial class AddedDeveloperIDforallparts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeveloperID",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeveloperID",
                table: "SubModules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeveloperID",
                table: "Modules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeveloperID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "DeveloperID",
                table: "SubModules");

            migrationBuilder.DropColumn(
                name: "DeveloperID",
                table: "Modules");
        }
    }
}
