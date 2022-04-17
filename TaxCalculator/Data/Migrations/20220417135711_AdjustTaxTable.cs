using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxCalculator.Data.Migrations
{
    public partial class AdjustTaxTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaxType",
                table: "Taxes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxType",
                table: "Taxes");
        }
    }
}
