using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class NewProduct1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_CategoriesTwo_CategoryFiveId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_CategoriesTwo_CategoryFourId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_CategoriesTwo_CategoryThreeId",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CategoriesFive_CategoryFiveId",
                table: "Products",
                column: "CategoryFiveId",
                principalTable: "CategoriesFive",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CategoriesFour_CategoryFourId",
                table: "Products",
                column: "CategoryFourId",
                principalTable: "CategoriesFour",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CategoriesThree_CategoryThreeId",
                table: "Products",
                column: "CategoryThreeId",
                principalTable: "CategoriesThree",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_CategoriesFive_CategoryFiveId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_CategoriesFour_CategoryFourId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_CategoriesThree_CategoryThreeId",
                table: "Products");

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
        }
    }
}
