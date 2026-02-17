using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class Pricelist2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandName",
                table: "Pricelists");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Pricelists");

            migrationBuilder.DropColumn(
                name: "CategoryTwoName",
                table: "Pricelists");

            migrationBuilder.DropColumn(
                name: "ColorName",
                table: "Pricelists");

            migrationBuilder.DropColumn(
                name: "LocName",
                table: "Pricelists");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Pricelists");

            migrationBuilder.CreateIndex(
                name: "IX_Pricelists_BrandId",
                table: "Pricelists",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Pricelists_CategoryId",
                table: "Pricelists",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Pricelists_CategoryTwoId",
                table: "Pricelists",
                column: "CategoryTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pricelists_ColorId",
                table: "Pricelists",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pricelists_LocationId",
                table: "Pricelists",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Pricelists_ProductId",
                table: "Pricelists",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pricelists_Brands_BrandId",
                table: "Pricelists",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pricelists_CategoriesTwo_CategoryTwoId",
                table: "Pricelists",
                column: "CategoryTwoId",
                principalTable: "CategoriesTwo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pricelists_Categories_CategoryId",
                table: "Pricelists",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pricelists_Colors_ColorId",
                table: "Pricelists",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pricelists_Locations_LocationId",
                table: "Pricelists",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pricelists_Products_ProductId",
                table: "Pricelists",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pricelists_Brands_BrandId",
                table: "Pricelists");

            migrationBuilder.DropForeignKey(
                name: "FK_Pricelists_CategoriesTwo_CategoryTwoId",
                table: "Pricelists");

            migrationBuilder.DropForeignKey(
                name: "FK_Pricelists_Categories_CategoryId",
                table: "Pricelists");

            migrationBuilder.DropForeignKey(
                name: "FK_Pricelists_Colors_ColorId",
                table: "Pricelists");

            migrationBuilder.DropForeignKey(
                name: "FK_Pricelists_Locations_LocationId",
                table: "Pricelists");

            migrationBuilder.DropForeignKey(
                name: "FK_Pricelists_Products_ProductId",
                table: "Pricelists");

            migrationBuilder.DropIndex(
                name: "IX_Pricelists_BrandId",
                table: "Pricelists");

            migrationBuilder.DropIndex(
                name: "IX_Pricelists_CategoryId",
                table: "Pricelists");

            migrationBuilder.DropIndex(
                name: "IX_Pricelists_CategoryTwoId",
                table: "Pricelists");

            migrationBuilder.DropIndex(
                name: "IX_Pricelists_ColorId",
                table: "Pricelists");

            migrationBuilder.DropIndex(
                name: "IX_Pricelists_LocationId",
                table: "Pricelists");

            migrationBuilder.DropIndex(
                name: "IX_Pricelists_ProductId",
                table: "Pricelists");

            migrationBuilder.AddColumn<string>(
                name: "BrandName",
                table: "Pricelists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "Pricelists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryTwoName",
                table: "Pricelists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorName",
                table: "Pricelists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocName",
                table: "Pricelists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Pricelists",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
