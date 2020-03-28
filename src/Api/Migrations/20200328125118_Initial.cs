using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hilfswerk.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Helfer",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Kontakt_Vorname = table.Column<string>(nullable: true),
                    Kontakt_Nachname = table.Column<string>(nullable: true),
                    Kontakt_Plz = table.Column<int>(nullable: true),
                    Kontakt_Strasse = table.Column<string>(nullable: true),
                    Kontakt_Telefon = table.Column<string>(nullable: true),
                    Kontakt_Email = table.Column<string>(nullable: true),
                    Kontakt_GeoLocation = table.Column<string>(nullable: true),
                    Anmerkung = table.Column<string>(maxLength: 2000, nullable: true),
                    hatAuto = table.Column<bool>(nullable: false),
                    istRisikogrupepe = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Helfer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Taetigkeit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taetigkeit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Einsaetze",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    HelferId = table.Column<string>(nullable: false),
                    TaetigkeitId = table.Column<int>(nullable: false),
                    Hilfesuchender = table.Column<string>(maxLength: 200, nullable: false),
                    VermitteltDurch = table.Column<string>(maxLength: 200, nullable: false),
                    VermitteltAm = table.Column<DateTime>(nullable: false),
                    Anmerkungen = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Einsaetze", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Einsaetze_Helfer_HelferId",
                        column: x => x.HelferId,
                        principalTable: "Helfer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Einsaetze_Taetigkeit_TaetigkeitId",
                        column: x => x.TaetigkeitId,
                        principalTable: "Taetigkeit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HelferTaetigkeit",
                columns: table => new
                {
                    HelferId = table.Column<string>(nullable: false),
                    TaetigkeitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelferTaetigkeit", x => new { x.HelferId, x.TaetigkeitId });
                    table.ForeignKey(
                        name: "FK_HelferTaetigkeit_Helfer_HelferId",
                        column: x => x.HelferId,
                        principalTable: "Helfer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HelferTaetigkeit_Taetigkeit_TaetigkeitId",
                        column: x => x.TaetigkeitId,
                        principalTable: "Taetigkeit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Taetigkeit",
                columns: new[] { "Id", "Label" },
                values: new object[] { 2, "Telefonkontakt" });

            migrationBuilder.InsertData(
                table: "Taetigkeit",
                columns: new[] { "Id", "Label" },
                values: new object[] { 4, "Gassi gehen" });

            migrationBuilder.InsertData(
                table: "Taetigkeit",
                columns: new[] { "Id", "Label" },
                values: new object[] { 1, "Besorgung" });

            migrationBuilder.InsertData(
                table: "Taetigkeit",
                columns: new[] { "Id", "Label" },
                values: new object[] { 8, "Andere" });

            migrationBuilder.CreateIndex(
                name: "IX_Einsaetze_HelferId",
                table: "Einsaetze",
                column: "HelferId");

            migrationBuilder.CreateIndex(
                name: "IX_Einsaetze_TaetigkeitId",
                table: "Einsaetze",
                column: "TaetigkeitId");

            migrationBuilder.CreateIndex(
                name: "IX_HelferTaetigkeit_TaetigkeitId",
                table: "HelferTaetigkeit",
                column: "TaetigkeitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Einsaetze");

            migrationBuilder.DropTable(
                name: "HelferTaetigkeit");

            migrationBuilder.DropTable(
                name: "Helfer");

            migrationBuilder.DropTable(
                name: "Taetigkeit");
        }
    }
}
