using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Infrastracture.Migrations
{
    public partial class updatecarts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "CartProducts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "CartProducts");
        }
    }
}
