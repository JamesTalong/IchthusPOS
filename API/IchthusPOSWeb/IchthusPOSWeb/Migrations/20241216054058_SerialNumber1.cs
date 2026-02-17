using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class SerialNumber1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pricelists_Brands_BrandId",
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

            migrationBuilder.DropColumn(
                name: "SerialNumbers",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "Sold",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "Unsold",
                table: "Batches");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Pricelists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Pricelists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ColorId",
                table: "Pricelists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Pricelists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "Pricelists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "SerialNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSold = table.Column<bool>(type: "bit", nullable: false),
                    BatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerialNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SerialNumbers_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumbers_BatchId",
                table: "SerialNumbers",
                column: "BatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pricelists_Brands_BrandId",
                table: "Pricelists",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pricelists_Categories_CategoryId",
                table: "Pricelists",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pricelists_Colors_ColorId",
                table: "Pricelists",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pricelists_Locations_LocationId",
                table: "Pricelists",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pricelists_Products_ProductId",
                table: "Pricelists",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pricelists_Brands_BrandId",
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

            migrationBuilder.DropTable(
                name: "SerialNumbers");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Pricelists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Pricelists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ColorId",
                table: "Pricelists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Pricelists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "Pricelists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerialNumbers",
                table: "Batches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sold",
                table: "Batches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unsold",
                table: "Batches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pricelists_Brands_BrandId",
                table: "Pricelists",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
