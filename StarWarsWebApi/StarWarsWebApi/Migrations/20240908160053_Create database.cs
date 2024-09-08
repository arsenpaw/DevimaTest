using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWarsWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Createdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StarWarsCharacters",
                columns: table => new
                {
                    PrivateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalApiId = table.Column<int>(type: "int", nullable: true),
                    Films = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Species = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Starships = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vehicles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Edited = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BirthYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EyeColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HairColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkinColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Homeworld = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarWarsCharacters", x => x.PrivateId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StarWarsCharacters_ExternalApiId",
                table: "StarWarsCharacters",
                column: "ExternalApiId",
                unique: true,
                filter: "[ExternalApiId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StarWarsCharacters_Name",
                table: "StarWarsCharacters",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StarWarsCharacters");
        }
    }
}
