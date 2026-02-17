using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class PurchasedProduct1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "PurchasedProducts",
                newName: "Subtotal");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "PurchasedProducts",
                newName: "Price");

            migrationBuilder.AddColumn<int>(
                name: "CustomerTempId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PurchasedProductId",
                table: "SerialTemps",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiscountValue",
                table: "PurchasedProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerType",
                table: "CustomerTemps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerTempId",
                table: "Transactions",
                column: "CustomerTempId");

            migrationBuilder.CreateIndex(
                name: "IX_SerialTemps_PurchasedProductId",
                table: "SerialTemps",
                column: "PurchasedProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SerialTemps_PurchasedProducts_PurchasedProductId",
                table: "SerialTemps",
                column: "PurchasedProductId",
                principalTable: "PurchasedProducts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_CustomerTemps_CustomerTempId",
                table: "Transactions",
                column: "CustomerTempId",
                principalTable: "CustomerTemps",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SerialTemps_PurchasedProducts_PurchasedProductId",
                table: "SerialTemps");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_CustomerTemps_CustomerTempId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CustomerTempId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_SerialTemps_PurchasedProductId",
                table: "SerialTemps");

            migrationBuilder.DropColumn(
                name: "CustomerTempId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PurchasedProductId",
                table: "SerialTemps");

            migrationBuilder.DropColumn(
                name: "DiscountValue",
                table: "PurchasedProducts");

            migrationBuilder.DropColumn(
                name: "CustomerType",
                table: "CustomerTemps");

            migrationBuilder.RenameColumn(
                name: "Subtotal",
                table: "PurchasedProducts",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "PurchasedProducts",
                newName: "TotalPrice");
        }
    }
}
