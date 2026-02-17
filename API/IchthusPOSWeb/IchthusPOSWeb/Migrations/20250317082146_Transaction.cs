using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class Transaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerType",
                table: "CustomerTemps");

            migrationBuilder.RenameColumn(
                name: "DiscountValue",
                table: "Transactions",
                newName: "DiscountAmount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountAmount",
                table: "Transactions",
                newName: "DiscountValue");

            migrationBuilder.AddColumn<string>(
                name: "CustomerType",
                table: "CustomerTemps",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
