using Microsoft.EntityFrameworkCore.Migrations;

namespace Hilfswerk.Api.Migrations
{
    public partial class _70timespandauer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dauer",
                table: "Einsaetze",
                nullable: true);
            migrationBuilder.Sql(
            @"
                UPDATE Einsaetze
                SET Dauer = stunden * 3600
            ");
            // Drop Column not supported by SQLite
            // https://docs.microsoft.com/en-us/ef/core/providers/sqlite/limitations
            //migrationBuilder.DropColumn(
            //    name: "Stunden",
            //    table: "Einsaetze");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop Column not supported by SQLite

            //migrationBuilder.AddColumn<int>(
            //    name: "Stunden",
            //    table: "Einsaetze",
            //    nullable: true);
            migrationBuilder.Sql(
            @"
                UPDATE Einsaetze
                SET Stunden = dauer / 3600
            ");
            ////migrationBuilder.DropColumn(
            ////    name: "Dauer",
            ////    table: "Einsaetze");
        }
    }
}
