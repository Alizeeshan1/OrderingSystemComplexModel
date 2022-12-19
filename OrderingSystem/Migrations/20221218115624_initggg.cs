using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderingSystem.Migrations
{
    public partial class initggg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderedItems",
                table: "OrderedItems");

            migrationBuilder.DropColumn(
                name: "PricePerUnit",
                table: "UnitItems");

            migrationBuilder.DropColumn(
                name: "Quatity",
                table: "UnitItems");

            migrationBuilder.AlterColumn<int>(
                name: "UnitId_Fk",
                table: "OrderedItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "OrderedItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "OrderedItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderedItems",
                table: "OrderedItems",
                columns: new[] { "OrderId_FK", "ItemId_Fk" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderedItems",
                table: "OrderedItems");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "OrderedItems");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "OrderedItems");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerUnit",
                table: "UnitItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quatity",
                table: "UnitItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UnitId_Fk",
                table: "OrderedItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderedItems",
                table: "OrderedItems",
                columns: new[] { "OrderId_FK", "ItemId_Fk", "UnitId_Fk" });
        }
    }
}
