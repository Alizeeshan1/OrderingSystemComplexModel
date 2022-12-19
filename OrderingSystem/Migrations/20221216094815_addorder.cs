using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderingSystem.Migrations
{
    public partial class addorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "OrderedItems");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderedItems");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "OrderedItems");

            migrationBuilder.DropColumn(
                name: "customerName",
                table: "OrderedItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "OrderedItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderedItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "OrderedItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "customerName",
                table: "OrderedItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
