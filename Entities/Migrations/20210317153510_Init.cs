using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbBrouwer",
                columns: table => new
                {
                    BrouwerNr = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrNaam = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Adres = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    PostCode = table.Column<short>(nullable: true),
                    Gemeente = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Omzet = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbBrouwer", x => x.BrouwerNr);
                });

            migrationBuilder.CreateTable(
                name: "Soort",
                columns: table => new
                {
                    SoortNr = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Soort = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Soort", x => x.SoortNr);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Voornaam = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Familienaam = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    GeboorteDatum = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    BierNr = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Bier",
                columns: table => new
                {
                    BierNr = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    BrouwerNr = table.Column<int>(nullable: true),
                    SoortNr = table.Column<int>(nullable: true),
                    Alcohol = table.Column<double>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bier", x => x.BierNr);
                    table.ForeignKey(
                        name: "FK_Bier_DbBrouwer_BrouwerNr",
                        column: x => x.BrouwerNr,
                        principalTable: "DbBrouwer",
                        principalColumn: "BrouwerNr",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bier_Soort_SoortNr",
                        column: x => x.SoortNr,
                        principalTable: "Soort",
                        principalColumn: "SoortNr",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bier_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "DbBrouwer",
                columns: new[] { "BrouwerNr", "Adres", "BrNaam", "Gemeente", "Omzet", "PostCode" },
                values: new object[] { 1, "ArtoisLaan 1", "Artois", "TestGemeente", 10000, (short)2500 });

            migrationBuilder.InsertData(
                table: "Soort",
                columns: new[] { "SoortNr", "Soort" },
                values: new object[] { 1, "Pils" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "BierNr", "Email", "Familienaam", "GeboorteDatum", "Voornaam" },
                values: new object[] { 1, null, "jos.deKlos@gmail.com", "De Klos", new DateTime(1974, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jos" });

            migrationBuilder.InsertData(
                table: "Bier",
                columns: new[] { "BierNr", "Alcohol", "BrouwerNr", "Naam", "SoortNr", "UserId" },
                values: new object[] { 1, 5.2000000000000002, 1, "Heineken", 1, null });

            migrationBuilder.CreateIndex(
                name: "IX_Bier_BrouwerNr",
                table: "Bier",
                column: "BrouwerNr");

            migrationBuilder.CreateIndex(
                name: "IX_Bier_SoortNr",
                table: "Bier",
                column: "SoortNr");

            migrationBuilder.CreateIndex(
                name: "IX_Bier_UserId",
                table: "Bier",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_BierNr",
                table: "User",
                column: "BierNr");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Bier_BierNr",
                table: "User",
                column: "BierNr",
                principalTable: "Bier",
                principalColumn: "BierNr",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bier_DbBrouwer_BrouwerNr",
                table: "Bier");

            migrationBuilder.DropForeignKey(
                name: "FK_Bier_Soort_SoortNr",
                table: "Bier");

            migrationBuilder.DropForeignKey(
                name: "FK_Bier_User_UserId",
                table: "Bier");

            migrationBuilder.DropTable(
                name: "DbBrouwer");

            migrationBuilder.DropTable(
                name: "Soort");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Bier");
        }
    }
}
