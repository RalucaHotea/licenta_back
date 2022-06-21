using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class UpdatePickupPointAndWarehouseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Warehouses",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "PickupPoints",
                newName: "Country");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "PickupPoints",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "City",
                table: "PickupPoints");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Warehouses",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "PickupPoints",
                newName: "Location");
        }
    }
}
