using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWarsWebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StarWarsCharacters_ExternalApiId",
                table: "StarWarsCharacters");

            migrationBuilder.AlterColumn<int>(
                name: "ExternalApiId",
                table: "StarWarsCharacters",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StarWarsCharacters_ExternalApiId",
                table: "StarWarsCharacters",
                column: "ExternalApiId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StarWarsCharacters_ExternalApiId",
                table: "StarWarsCharacters");

            migrationBuilder.AlterColumn<int>(
                name: "ExternalApiId",
                table: "StarWarsCharacters",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_StarWarsCharacters_ExternalApiId",
                table: "StarWarsCharacters",
                column: "ExternalApiId",
                unique: true,
                filter: "[ExternalApiId] IS NOT NULL");
        }
    }
}
