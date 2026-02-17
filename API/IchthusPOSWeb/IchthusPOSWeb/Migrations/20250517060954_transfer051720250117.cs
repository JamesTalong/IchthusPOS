using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class transfer051720250117 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TransferItem_PricelistId",
                table: "TransferItem",
                column: "PricelistId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItem_Pricelists_PricelistId",
                table: "TransferItem",
                column: "PricelistId",
                principalTable: "Pricelists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferItem_Pricelists_PricelistId",
                table: "TransferItem");

            migrationBuilder.DropIndex(
                name: "IX_TransferItem_PricelistId",
                table: "TransferItem");
        }
    }
}
