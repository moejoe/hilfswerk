using Microsoft.EntityFrameworkCore.Migrations;

namespace Hilfswerk.Api.Migrations
{
    public partial class _65dsgvokonform : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "istDSGVOKonform",
                table: "Helfer",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "istDSGVOKonform",
                table: "Helfer");
        }
    }
}
