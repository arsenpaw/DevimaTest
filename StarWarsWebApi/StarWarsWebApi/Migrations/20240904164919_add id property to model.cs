using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWarsWebApi.Migrations
{
    /// <inheritdoc />
    public partial class addidpropertytomodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExternalApiId",
                table: "Persons",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalApiId",
                table: "Persons");
        }
    }
}
