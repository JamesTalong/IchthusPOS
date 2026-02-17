using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class SerialTemp4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "SerialTemps");

            migrationBuilder.AddColumn<int>(
                name: "PricelistId",
                table: "SerialTemps",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SerialNumbers_SerialTemps_SerialTempId",
                table: "SerialNumbers");

            migrationBuilder.DropIndex(
                name: "IX_SerialNumbers_SerialTempId",
                table: "SerialNumbers");

            migrationBuilder.DropColumn(
                name: "PricelistId",
                table: "SerialTemps");

            migrationBuilder.DropColumn(
                name: "SerialTempId",
                table: "SerialNumbers");

            migrationBuilder.AddColumn<int>(
                name: "SerialNumber",
                table: "SerialTemps",
                type: "int",
                nullable: true);
        }
    }
}
