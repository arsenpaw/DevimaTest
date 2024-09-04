using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWarsWebApi.Migrations
{
    /// <inheritdoc />
    public partial class renmaeid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Persons_Id",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Persons",
                newName: "PrivateId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Persons",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Name",
                table: "Persons",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Persons_Name",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "PrivateId",
                table: "Persons",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Id",
                table: "Persons",
                column: "Id",
                unique: true);
        }
    }
}
