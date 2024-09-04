using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWarsWebApi.Migrations
{
    /// <inheritdoc />
    public partial class addindextonumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Persons_ExternalApiId",
                table: "Persons",
                column: "ExternalApiId",
                unique: true,
                filter: "[ExternalApiId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Persons_ExternalApiId",
                table: "Persons");
        }
    }
}
