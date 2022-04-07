using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class AddDraftEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductWarehouseMapping_ProductId",
                table: "ProductWarehouseMapping",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWarehouseMapping_WarehouseId",
                table: "ProductWarehouseMapping",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWarehouseMapping_Products_ProductId",
                table: "ProductWarehouseMapping",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWarehouseMapping_Warehouses_WarehouseId",
                table: "ProductWarehouseMapping",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductWarehouseMapping_Products_ProductId",
                table: "ProductWarehouseMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductWarehouseMapping_Warehouses_WarehouseId",
                table: "ProductWarehouseMapping");

            migrationBuilder.DropIndex(
                name: "IX_ProductWarehouseMapping_ProductId",
                table: "ProductWarehouseMapping");

            migrationBuilder.DropIndex(
                name: "IX_ProductWarehouseMapping_WarehouseId",
                table: "ProductWarehouseMapping");
        }
    }
}
