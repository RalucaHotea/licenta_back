using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class AddPickupPointEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Orders",
                newName: "PickupPointId");

            migrationBuilder.CreateTable(
                name: "PickupPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickupPoints", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PickupPoints");

            migrationBuilder.RenameColumn(
                name: "PickupPointId",
                table: "Orders",
                newName: "Status");

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
