using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class transferItem052220250220 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SerialNumberId",
                table: "CompletedTransferSerials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CompletedTransferSerials_SerialNumberId",
                table: "CompletedTransferSerials",
                column: "SerialNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedTransferItems_PricelistId",
                table: "CompletedTransferItems",
                column: "PricelistId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedTransferItems_Pricelists_PricelistId",
                table: "CompletedTransferItems",
                column: "PricelistId",
                principalTable: "Pricelists",
                principalColumn: "Id");

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
                name: "FK_CompletedTransferItems_Pricelists_PricelistId",
                table: "CompletedTransferItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CompletedTransferSerials_SerialNumbers_SerialNumberId",
                table: "CompletedTransferSerials");

            migrationBuilder.DropIndex(
                name: "IX_CompletedTransferSerials_SerialNumberId",
                table: "CompletedTransferSerials");

            migrationBuilder.DropIndex(
                name: "IX_CompletedTransferItems_PricelistId",
                table: "CompletedTransferItems");

            migrationBuilder.DropColumn(
                name: "SerialNumberId",
                table: "CompletedTransferSerials");
        }
    }
}
