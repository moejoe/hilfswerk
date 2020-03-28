using Microsoft.EntityFrameworkCore.Migrations;

namespace Hilfswerk.Api.Migrations
{
    public partial class _32zivisundfreiwillige : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "istFreiwilliger",
                table: "Helfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "istZivildiener",
                table: "Helfer",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "istFreiwilliger",
                table: "Helfer");

            migrationBuilder.DropColumn(
                name: "istZivildiener",
                table: "Helfer");
        }
    }
}
