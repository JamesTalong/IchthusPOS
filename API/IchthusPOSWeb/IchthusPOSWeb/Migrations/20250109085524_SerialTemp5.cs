using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class SerialTemp5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SerialNumbers_SerialTemps_SerialTempId",
                table: "SerialNumbers");

            migrationBuilder.DropIndex(
                name: "IX_SerialNumbers_SerialTempId",
                table: "SerialNumbers");

            migrationBuilder.DropColumn(
                name: "SerialTempId",
                table: "SerialNumbers");

            migrationBuilder.AddColumn<string>(
                name: "SerialNumbers",
                table: "SerialTemps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNumbers",
                table: "SerialTemps");

            migrationBuilder.AddColumn<int>(
                name: "SerialTempId",
                table: "SerialNumbers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumbers_SerialTempId",
                table: "SerialNumbers",
                column: "SerialTempId");

            migrationBuilder.AddForeignKey(
                name: "FK_SerialNumbers_SerialTemps_SerialTempId",
                table: "SerialNumbers",
                column: "SerialTempId",
                principalTable: "SerialTemps",
                principalColumn: "Id");
        }
    }
}
