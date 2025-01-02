using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Infrastracture.Migrations
{
    public partial class changenames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                newName: "OrderProducts");

            migrationBuilder.RenameTable(
                name: "CartItems",
                newName: "ProductsCarts");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_ProductId",
                table: "ProductsCarts",
                newName: "IX_ProductsCarts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_CartId",
                table: "ProductsCarts",
                newName: "IX_ProductsCarts_CartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsCarts",
                table: "ProductsCarts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsCarts_Carts_CartId",
                table: "ProductsCarts",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsCarts_Products_ProductId",
                table: "ProductsCarts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsCarts_Carts_CartId",
                table: "ProductsCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsCarts_Products_ProductId",
                table: "ProductsCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsCarts",
                table: "ProductsCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts");

            migrationBuilder.RenameTable(
                name: "ProductsCarts",
                newName: "CartItems");

            migrationBuilder.RenameTable(
                name: "OrderProducts",
                newName: "OrderItems");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsCarts_ProductId",
                table: "CartItems",
                newName: "IX_CartItems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsCarts_CartId",
                table: "CartItems",
                newName: "IX_CartItems_CartId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderItems",
                newName: "IX_OrderItems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
