using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWarsWebApi.Migrations
{
    public partial class makeidguid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Drop the primary key constraint or any other constraints that depend on PrivateId
            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            // Drop any additional constraints or indexes related to PrivateId here if needed

            // Step 2: Add a temporary column of type Guid
            migrationBuilder.AddColumn<Guid>(
                name: "TempPrivateId",
                table: "Persons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.NewGuid());

            // Step 3: Drop the old column
            migrationBuilder.DropColumn(
                name: "PrivateId",
                table: "Persons");

            // Step 4: Rename the new column to match the old column name
            migrationBuilder.RenameColumn(
                name: "TempPrivateId",
                table: "Persons",
                newName: "PrivateId");

            // Step 5: Recreate the primary key or any other constraints that were dropped
            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "PrivateId");

            // Add back any additional constraints or indexes related to PrivateId here if needed
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverse the changes to recreate the original state

            // Drop the primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            // Drop the GUID column
            migrationBuilder.DropColumn(
                name: "PrivateId",
                table: "Persons");

            // Recreate the original int column with IDENTITY property
            migrationBuilder.AddColumn<int>(
                name: "PrivateId",
                table: "Persons",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            // Recreate the primary key
            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "PrivateId");
        }
    }
}
