using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class StagingBatch041620251049 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BatchStagings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NumberOfItems = table.Column<int>(type: "int", nullable: true),
                    PricelistId = table.Column<int>(type: "int", nullable: false),
                    HasSerial = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchStagings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchStagings_Pricelists_PricelistId",
                        column: x => x.PricelistId,
                        principalTable: "Pricelists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SerialStagings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSold = table.Column<bool>(type: "bit", nullable: false),
                    BatchStagingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerialStagings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SerialStagings_BatchStagings_BatchStagingId",
                        column: x => x.BatchStagingId,
                        principalTable: "BatchStagings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BatchStagings_PricelistId",
                table: "BatchStagings",
                column: "PricelistId");

            migrationBuilder.CreateIndex(
                name: "IX_SerialStagings_BatchStagingId",
                table: "SerialStagings",
                column: "BatchStagingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SerialStagings");

            migrationBuilder.DropTable(
                name: "BatchStagings");
        }
    }
}
