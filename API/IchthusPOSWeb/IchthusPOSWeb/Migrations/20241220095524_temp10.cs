using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class temp10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SerialNumbers_TempSerials_TempSerialId",
                table: "SerialNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_TempSerials_Batches_BatchId",
                table: "TempSerials");

            migrationBuilder.DropIndex(
                name: "IX_TempSerials_BatchId",
                table: "TempSerials");

            migrationBuilder.DropIndex(
                name: "IX_SerialNumbers_TempSerialId",
                table: "SerialNumbers");

            migrationBuilder.DropColumn(
                name: "TempSerialId",
                table: "SerialNumbers");

            migrationBuilder.AddColumn<string>(
                name: "SerialNumbers",
                table: "TempSerials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNumbers",
                table: "TempSerials");

            migrationBuilder.AddColumn<int>(
                name: "TempSerialId",
                table: "SerialNumbers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TempSerials_BatchId",
                table: "TempSerials",
                column: "BatchId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_TempSerials_Batches_BatchId",
                table: "TempSerials",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
