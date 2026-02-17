using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class StagingHistory04262025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BatchStagingsHistory",
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
                    table.PrimaryKey("PK_BatchStagingsHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchStagingsHistory_Pricelists_PricelistId",
                        column: x => x.PricelistId,
                        principalTable: "Pricelists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SerialStagingsHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSold = table.Column<bool>(type: "bit", nullable: false),
                    BatchStagingId = table.Column<int>(type: "int", nullable: true),
                    BatchStaginghistoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerialStagingsHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SerialStagingsHistory_BatchStagingsHistory_BatchStaginghistoryId",
                        column: x => x.BatchStaginghistoryId,
                        principalTable: "BatchStagingsHistory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BatchStagingsHistory_PricelistId",
                table: "BatchStagingsHistory",
                column: "PricelistId");

            migrationBuilder.CreateIndex(
                name: "IX_SerialStagingsHistory_BatchStaginghistoryId",
                table: "SerialStagingsHistory",
                column: "BatchStaginghistoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SerialStagingsHistory");

            migrationBuilder.DropTable(
                name: "BatchStagingsHistory");
        }
    }
}
