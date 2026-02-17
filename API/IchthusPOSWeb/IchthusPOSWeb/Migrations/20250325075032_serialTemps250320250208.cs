using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class serialTemps250320250208 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SerialTemps_PurchasedProducts_PurchasedProductId",
                table: "SerialTemps");

            migrationBuilder.DropIndex(
                name: "IX_SerialTemps_PurchasedProductId",
                table: "SerialTemps");

            migrationBuilder.DropColumn(
                name: "PurchasedProductId",
                table: "SerialTemps");

            migrationBuilder.AddColumn<int>(
                name: "PurchasedProductId",
                table: "SerialMains",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SerialMains_PurchasedProductId",
                table: "SerialMains",
                column: "PurchasedProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SerialMains_PurchasedProducts_PurchasedProductId",
                table: "SerialMains",
                column: "PurchasedProductId",
                principalTable: "PurchasedProducts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SerialMains_PurchasedProducts_PurchasedProductId",
                table: "SerialMains");

            migrationBuilder.DropIndex(
                name: "IX_SerialMains_PurchasedProductId",
                table: "SerialMains");

            migrationBuilder.DropColumn(
                name: "PurchasedProductId",
                table: "SerialMains");

            migrationBuilder.AddColumn<int>(
                name: "PurchasedProductId",
                table: "SerialTemps",
                type: "int",
                nullable: true);

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
        }
    }
}
