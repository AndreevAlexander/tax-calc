using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxCalculator.Data.Migrations
{
    public partial class AlterIncomeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxProfiles_Currencies_ProfileCurrencyId",
                table: "TaxProfiles");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfileCurrencyId",
                table: "TaxProfiles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "IncomeDate",
                table: "Incomes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_TaxProfiles_Currencies_ProfileCurrencyId",
                table: "TaxProfiles",
                column: "ProfileCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxProfiles_Currencies_ProfileCurrencyId",
                table: "TaxProfiles");

            migrationBuilder.DropColumn(
                name: "IncomeDate",
                table: "Incomes");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfileCurrencyId",
                table: "TaxProfiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxProfiles_Currencies_ProfileCurrencyId",
                table: "TaxProfiles",
                column: "ProfileCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
