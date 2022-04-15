using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class AddShippingRelatedDatesToOrderEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Orders_PickupPointId",
                table: "Orders",
                column: "PickupPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PickupPoints_PickupPointId",
                table: "Orders",
                column: "PickupPointId",
                principalTable: "PickupPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PickupPoints_PickupPointId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PickupPointId",
                table: "Orders");
        }
    }
}
