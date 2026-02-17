using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class jobtitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InventoryCost",
                table: "JobRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TransferItems",
                table: "JobRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InventoryCost",
                table: "JobRoles");

            migrationBuilder.DropColumn(
                name: "TransferItems",
                table: "JobRoles");
        }
    }
}
