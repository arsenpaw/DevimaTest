using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWarsWebApi.Migrations
{
    /// <inheritdoc />
    public partial class adddb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Edited = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EyeColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HairColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkinColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Homeworld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Films = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Species = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Starships = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vehicles = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Id",
                table: "Persons",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
