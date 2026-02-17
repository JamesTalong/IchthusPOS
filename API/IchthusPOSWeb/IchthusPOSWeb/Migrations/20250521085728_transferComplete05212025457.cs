using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class transferComplete05212025457 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompletedTransfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransferId = table.Column<int>(type: "int", nullable: true),
                    FromLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiveBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecievedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedTransfers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompletedTransferItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompletedTransferId = table.Column<int>(type: "int", nullable: true),
                    ReceiverPricelistId = table.Column<int>(type: "int", nullable: true),
                    PricelistId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedTransferItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletedTransferItems_CompletedTransfers_CompletedTransferId",
                        column: x => x.CompletedTransferId,
                        principalTable: "CompletedTransfers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompletedTransferSerials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompletedTransferItemId = table.Column<int>(type: "int", nullable: true),
                    SerialId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompletedTransferItemsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedTransferSerials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletedTransferSerials_CompletedTransferItems_CompletedTransferItemsId",
                        column: x => x.CompletedTransferItemsId,
                        principalTable: "CompletedTransferItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedTransferItems_CompletedTransferId",
                table: "CompletedTransferItems",
                column: "CompletedTransferId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedTransferSerials_CompletedTransferItemsId",
                table: "CompletedTransferSerials",
                column: "CompletedTransferItemsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedTransferSerials");

            migrationBuilder.DropTable(
                name: "CompletedTransferItems");

            migrationBuilder.DropTable(
                name: "CompletedTransfers");
        }
    }
}
