using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWarsWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StarWarsCharacters",
                table: "StarWarsCharacters");

            migrationBuilder.RenameTable(
                name: "StarWarsCharacters",
                newName: "StarWarsWebCharacters");

            migrationBuilder.RenameIndex(
                name: "IX_StarWarsCharacters_PrivateId",
                table: "StarWarsWebCharacters",
                newName: "IX_StarWarsWebCharacters_PrivateId");

            migrationBuilder.RenameIndex(
                name: "IX_StarWarsCharacters_Name",
                table: "StarWarsWebCharacters",
                newName: "IX_StarWarsWebCharacters_Name");

            migrationBuilder.RenameIndex(
                name: "IX_StarWarsCharacters_ExternalApiId",
                table: "StarWarsWebCharacters",
                newName: "IX_StarWarsWebCharacters_ExternalApiId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StarWarsWebCharacters",
                table: "StarWarsWebCharacters",
                column: "PrivateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StarWarsWebCharacters",
                table: "StarWarsWebCharacters");

            migrationBuilder.RenameTable(
                name: "StarWarsWebCharacters",
                newName: "StarWarsCharacters");

            migrationBuilder.RenameIndex(
                name: "IX_StarWarsWebCharacters_PrivateId",
                table: "StarWarsCharacters",
                newName: "IX_StarWarsCharacters_PrivateId");

            migrationBuilder.RenameIndex(
                name: "IX_StarWarsWebCharacters_Name",
                table: "StarWarsCharacters",
                newName: "IX_StarWarsCharacters_Name");

            migrationBuilder.RenameIndex(
                name: "IX_StarWarsWebCharacters_ExternalApiId",
                table: "StarWarsCharacters",
                newName: "IX_StarWarsCharacters_ExternalApiId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StarWarsCharacters",
                table: "StarWarsCharacters",
                column: "PrivateId");
        }
    }
}
