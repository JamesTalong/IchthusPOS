using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class transferItem052220250223 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedTransferSerials_SerialNumbers_SerialNumberId",
                table: "CompletedTransferSerials");

            migrationBuilder.AlterColumn<int>(
                name: "SerialNumberId",
                table: "CompletedTransferSerials",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedTransferSerials_SerialNumbers_SerialNumberId",
                table: "CompletedTransferSerials",
                column: "SerialNumberId",
                principalTable: "SerialNumbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedTransferSerials_SerialNumbers_SerialNumberId",
                table: "CompletedTransferSerials");

            migrationBuilder.AlterColumn<int>(
                name: "SerialNumberId",
                table: "CompletedTransferSerials",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedTransferSerials_SerialNumbers_SerialNumberId",
                table: "CompletedTransferSerials",
                column: "SerialNumberId",
                principalTable: "SerialNumbers",
                principalColumn: "Id");
        }
    }
}
