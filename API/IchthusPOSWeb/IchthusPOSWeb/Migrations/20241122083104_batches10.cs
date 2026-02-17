using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class batches10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocName",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Batches");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_LocationId",
                table: "Batches",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_ProductId",
                table: "Batches",
                column: "ProductId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_Batches_ProductId",
                table: "Batches");

            migrationBuilder.AddColumn<string>(
                name: "LocName",
                table: "Batches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Batches",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
