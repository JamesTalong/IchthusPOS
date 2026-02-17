using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class SerialTemp2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_serialTemps",
                table: "serialTemps");

            migrationBuilder.RenameTable(
                name: "serialTemps",
                newName: "SerialTemps");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SerialTemps",
                table: "SerialTemps",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SerialTemps",
                table: "SerialTemps");

            migrationBuilder.RenameTable(
                name: "SerialTemps",
                newName: "serialTemps");

            migrationBuilder.AddPrimaryKey(
                name: "PK_serialTemps",
                table: "serialTemps",
                column: "Id");
        }
    }
}
