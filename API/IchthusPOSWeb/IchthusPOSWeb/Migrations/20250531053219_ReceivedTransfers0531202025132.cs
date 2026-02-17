using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class ReceivedTransfers0531202025132 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReceivedTransfers",
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
                    table.PrimaryKey("PK_ReceivedTransfers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceivedTransferItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompletedTransferId = table.Column<int>(type: "int", nullable: true),
                    ReceiverPricelistId = table.Column<int>(type: "int", nullable: true),
                    PricelistId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    ReceivedTransferId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivedTransferItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceivedTransferItems_Pricelists_PricelistId",
                        column: x => x.PricelistId,
                        principalTable: "Pricelists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceivedTransferItems_ReceivedTransfers_ReceivedTransferId",
                        column: x => x.ReceivedTransferId,
                        principalTable: "ReceivedTransfers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReceivedTransferSerials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumberId = table.Column<int>(type: "int", nullable: true),
                    SerialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedTransferItemsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivedTransferSerials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceivedTransferSerials_ReceivedTransferItems_ReceivedTransferItemsId",
                        column: x => x.ReceivedTransferItemsId,
                        principalTable: "ReceivedTransferItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceivedTransferSerials_SerialNumbers_SerialNumberId",
                        column: x => x.SerialNumberId,
                        principalTable: "SerialNumbers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceivedTransferItems_PricelistId",
                table: "ReceivedTransferItems",
                column: "PricelistId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivedTransferItems_ReceivedTransferId",
                table: "ReceivedTransferItems",
                column: "ReceivedTransferId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivedTransferSerials_ReceivedTransferItemsId",
                table: "ReceivedTransferSerials",
                column: "ReceivedTransferItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivedTransferSerials_SerialNumberId",
                table: "ReceivedTransferSerials",
                column: "SerialNumberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceivedTransferSerials");

            migrationBuilder.DropTable(
                name: "ReceivedTransferItems");

            migrationBuilder.DropTable(
                name: "ReceivedTransfers");
        }
    }
}
