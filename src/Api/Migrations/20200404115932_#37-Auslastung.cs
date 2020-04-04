using Microsoft.EntityFrameworkCore.Migrations;

namespace Hilfswerk.Api.Migrations
{
    public partial class _37Auslastung : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "istAusgelastet",
                table: "Helfer",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "istAusgelastet",
                table: "Helfer");
        }
    }
}
