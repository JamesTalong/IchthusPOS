using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class transferItem052220250222 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedTransferSerials_SerialNumbers_SerialId",
                table: "CompletedTransferSerials");

            migrationBuilder.DropIndex(
                name: "IX_CompletedTransferSerials_SerialId",
                table: "CompletedTransferSerials");

            migrationBuilder.AddColumn<int>(
                name: "SerialNumberId",
                table: "CompletedTransferSerials",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompletedTransferSerials_SerialNumberId",
                table: "CompletedTransferSerials",
                column: "SerialNumberId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedTransferSerials_SerialNumbers_SerialNumberId",
                table: "CompletedTransferSerials",
                column: "SerialNumberId",
                principalTable: "SerialNumbers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedTransferSerials_SerialNumbers_SerialNumberId",
                table: "CompletedTransferSerials");

            migrationBuilder.DropIndex(
                name: "IX_CompletedTransferSerials_SerialNumberId",
                table: "CompletedTransferSerials");

            migrationBuilder.DropColumn(
                name: "SerialNumberId",
                table: "CompletedTransferSerials");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedTransferSerials_SerialId",
                table: "CompletedTransferSerials",
                column: "SerialId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedTransferSerials_SerialNumbers_SerialId",
                table: "CompletedTransferSerials",
                column: "SerialId",
                principalTable: "SerialNumbers",
                principalColumn: "Id");
        }
    }
}
