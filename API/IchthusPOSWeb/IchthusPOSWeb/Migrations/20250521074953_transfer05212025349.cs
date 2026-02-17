using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class transfer05212025349 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferItem_Pricelists_PricelistId",
                table: "TransferItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItem_Transfers_TransferId",
                table: "TransferItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransferItem",
                table: "TransferItem");

            migrationBuilder.RenameTable(
                name: "TransferItem",
                newName: "TransferItems");

            migrationBuilder.RenameIndex(
                name: "IX_TransferItem_TransferId",
                table: "TransferItems",
                newName: "IX_TransferItems_TransferId");

            migrationBuilder.RenameIndex(
                name: "IX_TransferItem_PricelistId",
                table: "TransferItems",
                newName: "IX_TransferItems_PricelistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransferItems",
                table: "TransferItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItems_Pricelists_PricelistId",
                table: "TransferItems",
                column: "PricelistId",
                principalTable: "Pricelists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItems_Transfers_TransferId",
                table: "TransferItems",
                column: "TransferId",
                principalTable: "Transfers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferItems_Pricelists_PricelistId",
                table: "TransferItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItems_Transfers_TransferId",
                table: "TransferItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransferItems",
                table: "TransferItems");

            migrationBuilder.RenameTable(
                name: "TransferItems",
                newName: "TransferItem");

            migrationBuilder.RenameIndex(
                name: "IX_TransferItems_TransferId",
                table: "TransferItem",
                newName: "IX_TransferItem_TransferId");

            migrationBuilder.RenameIndex(
                name: "IX_TransferItems_PricelistId",
                table: "TransferItem",
                newName: "IX_TransferItem_PricelistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransferItem",
                table: "TransferItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItem_Pricelists_PricelistId",
                table: "TransferItem",
                column: "PricelistId",
                principalTable: "Pricelists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItem_Transfers_TransferId",
                table: "TransferItem",
                column: "TransferId",
                principalTable: "Transfers",
                principalColumn: "Id");
        }
    }
}
