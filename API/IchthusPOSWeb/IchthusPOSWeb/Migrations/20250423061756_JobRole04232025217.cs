using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IchthusPOSWeb.Migrations
{
    /// <inheritdoc />
    public partial class JobRole04232025217 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dashboard = table.Column<bool>(type: "bit", nullable: false),
                    Users = table.Column<bool>(type: "bit", nullable: false),
                    UserRestriction = table.Column<bool>(type: "bit", nullable: false),
                    Categories = table.Column<bool>(type: "bit", nullable: false),
                    Categories2 = table.Column<bool>(type: "bit", nullable: false),
                    Categories3 = table.Column<bool>(type: "bit", nullable: false),
                    Categories4 = table.Column<bool>(type: "bit", nullable: false),
                    Categories5 = table.Column<bool>(type: "bit", nullable: false),
                    Brands = table.Column<bool>(type: "bit", nullable: false),
                    Colors = table.Column<bool>(type: "bit", nullable: false),
                    Locations = table.Column<bool>(type: "bit", nullable: false),
                    ProductList = table.Column<bool>(type: "bit", nullable: false),
                    Pricelists = table.Column<bool>(type: "bit", nullable: false),
                    Batches = table.Column<bool>(type: "bit", nullable: false),
                    SerialNumbers = table.Column<bool>(type: "bit", nullable: false),
                    Customers = table.Column<bool>(type: "bit", nullable: false),
                    Inventory = table.Column<bool>(type: "bit", nullable: false),
                    InventoryStaging = table.Column<bool>(type: "bit", nullable: false),
                    Transactions = table.Column<bool>(type: "bit", nullable: false),
                    POS = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRoles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobRoles");
        }
    }
}
