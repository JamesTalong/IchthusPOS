using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class Batches20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batches_Locations_LocationId",
                table: "Batches");

            migrationBuilder.DropForeignKey(
                name: "FK_Batches_Products_ProductId",
                table: "Batches");

            migrationBuilder.DropIndex(
                name: "IX_Batches_LocationId",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Batches");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Batches",
                newName: "PricelistId");

            migrationBuilder.RenameIndex(
                name: "IX_Batches_ProductId",
                table: "Batches",
                newName: "IX_Batches_PricelistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_Pricelists_PricelistId",
                table: "Batches",
                column: "PricelistId",
                principalTable: "Pricelists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batches_Pricelists_PricelistId",
                table: "Batches");

            migrationBuilder.RenameColumn(
                name: "PricelistId",
                table: "Batches",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Batches_PricelistId",
                table: "Batches",
                newName: "IX_Batches_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Batches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Batches_LocationId",
                table: "Batches",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_Locations_LocationId",
                table: "Batches",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_Products_ProductId",
                table: "Batches",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
