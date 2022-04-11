using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class AddCartItemEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_UserCartEntityId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Orders_OrderEntityId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_OrderEntityId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_UserCartEntityId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "OrderEntityId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "UserCartEntityId",
                table: "CartItems");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems");

            migrationBuilder.AddColumn<int>(
                name: "OrderEntityId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserCartEntityId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_OrderEntityId",
                table: "CartItems",
                column: "OrderEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserCartEntityId",
                table: "CartItems",
                column: "UserCartEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_UserCartEntityId",
                table: "CartItems",
                column: "UserCartEntityId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Orders_OrderEntityId",
                table: "CartItems",
                column: "OrderEntityId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
