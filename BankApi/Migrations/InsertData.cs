using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankApi.Migrations
{
    public partial class InsertData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Amount", "Owner" },
                values: new object[] { new Guid("bc117f5a-da78-4e12-9873-9cd6c616d5f9"), 4200, "account-owner@andersmarchsteiner.onmicrosoft.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("bc117f5a-da78-4e12-9873-9cd6c616d5f9"));
        }
    }
}
