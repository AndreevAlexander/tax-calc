using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxCalculator.Data.Migrations
{
    public partial class AddCurrencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProfileCurrencyId",
                table: "TaxProfiles",
                type: "uniqueidentifier",
                nullable: true,
                defaultValue: null);

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExchangeRate = table.Column<double>(type: "float", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxProfiles_ProfileCurrencyId",
                table: "TaxProfiles",
                column: "ProfileCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxProfiles_Currencies_ProfileCurrencyId",
                table: "TaxProfiles",
                column: "ProfileCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[Currencies] (Id, Name, ExchangeRate, CreatedDate, UpdatedDate)
                VALUES 
                ('fc5ee427-961d-424f-858b-a108b5ee74fe', 'Usd', 27, GETDATE(), null),
                ('77f897e4-aca0-4f33-ad87-b630f55e1d11', 'Eur', 33, GETDATE(), null),
                ('366a90e0-6200-40ac-85a5-7951d587d7b4', 'Uah', 1, GETDATE(), null),
                ('2ff82fd1-d11c-444f-9fca-44cb4adb9b78', 'Pln', 6.8, GETDATE(), null);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxProfiles_Currencies_ProfileCurrencyId",
                table: "TaxProfiles");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_TaxProfiles_ProfileCurrencyId",
                table: "TaxProfiles");

            migrationBuilder.DropColumn(
                name: "ProfileCurrencyId",
                table: "TaxProfiles");
        }
    }
}
