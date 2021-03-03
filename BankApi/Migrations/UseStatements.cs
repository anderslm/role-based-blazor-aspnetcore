using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankApi.Migrations
{
    public partial class UseStatements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.CreateTable(
                name: "AccountStatements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStatements", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AccountStatements",
                columns: new[] { "Id", "Amount", "Owner", "Timestamp" },
                values: new object[] { new Guid("0d700d0c-ce7f-4f37-9e74-e3776a214a45"), 4200, "account-owner@andersmarchsteiner.onmicrosoft.com", new DateTimeOffset(new DateTime(2021, 3, 3, 9, 9, 19, 489, DateTimeKind.Unspecified).AddTicks(172), new TimeSpan(0, 1, 0, 0, 0)) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountStatements");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Amount", "Owner" },
                values: new object[] { new Guid("bc117f5a-da78-4e12-9873-9cd6c616d5f9"), 4200, "account-owner@andersmarchsteiner.onmicrosoft.com" });
        }
    }
}
