using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTempSerial1221202400420 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SerialNumbers_TempSerials_TempSerialId",
                table: "SerialNumbers");

            migrationBuilder.DropIndex(
                name: "IX_SerialNumbers_TempSerialId",
                table: "SerialNumbers");

            migrationBuilder.DropColumn(
                name: "TempSerialId",
                table: "SerialNumbers");

            migrationBuilder.CreateTable(
                name: "Serials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TempSerialId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Serials_TempSerials_TempSerialId",
                        column: x => x.TempSerialId,
                        principalTable: "TempSerials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Serials_TempSerialId",
                table: "Serials",
                column: "TempSerialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Serials");

            migrationBuilder.AddColumn<int>(
                name: "TempSerialId",
                table: "SerialNumbers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumbers_TempSerialId",
                table: "SerialNumbers",
                column: "TempSerialId");

            migrationBuilder.AddForeignKey(
                name: "FK_SerialNumbers_TempSerials_TempSerialId",
                table: "SerialNumbers",
                column: "TempSerialId",
                principalTable: "TempSerials",
                principalColumn: "Id");
        }
    }
}
