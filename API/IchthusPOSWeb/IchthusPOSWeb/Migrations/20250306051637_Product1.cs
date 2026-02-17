using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class Product1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_Pricelists_BrandId",
                table: "Pricelists");

            migrationBuilder.DropIndex(
                name: "IX_Pricelists_CategoryId",
                table: "Pricelists");

            migrationBuilder.DropIndex(
                name: "IX_Pricelists_CategoryTwoId",
                table: "Pricelists");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Pricelists");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Pricelists");

            migrationBuilder.DropColumn(
                name: "CategoryTwoId",
                table: "Pricelists");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryFiveId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryFourId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryThreeId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryTwoId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryFiveId",
                table: "Products",
                column: "CategoryFiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryFourId",
                table: "Products",
                column: "CategoryFourId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryThreeId",
                table: "Products",
                column: "CategoryThreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryTwoId",
                table: "Products",
                column: "CategoryTwoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CategoriesTwo_CategoryFiveId",
                table: "Products",
                column: "CategoryFiveId",
                principalTable: "CategoriesTwo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CategoriesTwo_CategoryFourId",
                table: "Products",
                column: "CategoryFourId",
                principalTable: "CategoriesTwo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CategoriesTwo_CategoryThreeId",
                table: "Products",
                column: "CategoryThreeId",
                principalTable: "CategoriesTwo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CategoriesTwo_CategoryTwoId",
                table: "Products",
                column: "CategoryTwoId",
                principalTable: "CategoriesTwo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_CategoriesTwo_CategoryFiveId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_CategoriesTwo_CategoryFourId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_CategoriesTwo_CategoryThreeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_CategoriesTwo_CategoryTwoId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrandId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryFiveId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryFourId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryThreeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryTwoId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryFiveId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryFourId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryThreeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryTwoId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Pricelists",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Pricelists",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryTwoId",
                table: "Pricelists",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Pricelists_Brands_BrandId",
                table: "Pricelists",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");

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
                principalColumn: "Id");
        }
    }
}
