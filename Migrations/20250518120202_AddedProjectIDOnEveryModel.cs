using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumApp__Juro_.Migrations
{
    /// <inheritdoc />
    public partial class AddedProjectIDOnEveryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectID",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectID",
                table: "SubModules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "SubModules");
        }
    }
}
