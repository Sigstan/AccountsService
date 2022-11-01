using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountsService.Storage.Migrations
{
    public partial class CreateCashbackConfigurationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CashbackConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountLevel = table.Column<short>(type: "smallint", nullable: false),
                    CashbackPercentange = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashbackConfigurations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CashbackConfigurations",
                columns: new[] { "Id", "AccountLevel", "CashbackPercentange" },
                values: new object[] { new Guid("53bbd840-4032-7f6e-1773-aac91540702a"), (short)1, 0m });

            migrationBuilder.InsertData(
                table: "CashbackConfigurations",
                columns: new[] { "Id", "AccountLevel", "CashbackPercentange" },
                values: new object[] { new Guid("87bbd860-6098-4f6e-9353-5c911409305c"), (short)2, 1m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashbackConfigurations");
        }
    }
}
