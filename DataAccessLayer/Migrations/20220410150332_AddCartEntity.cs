using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class AddCartEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CartItems",
                newName: "CartId");

            migrationBuilder.AddColumn<int>(
                name: "UserCartEntityId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_UserCartEntityId",
                table: "CartItems");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_UserCartEntityId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "UserCartEntityId",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "CartItems",
                newName: "UserId");
        }
    }
}
