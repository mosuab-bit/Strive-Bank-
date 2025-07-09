using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedBranchesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CreditCards",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 9, 20, 5, 5, 882, DateTimeKind.Local).AddTicks(2895),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 4, 23, 18, 42, 24, 450, DateTimeKind.Local).AddTicks(2164));

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "BranchId", "BranchLocation", "BranchName" },
                values: new object[,]
                {
                    { 1, "middle Towmn ", "MainBranch" },
                    { 2, "middle Towmn ", "WestBranch" },
                    { 3, "middle Towmn ", "EastBranch" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "BranchId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "BranchId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "BranchId",
                keyValue: 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CreditCards",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 23, 18, 42, 24, 450, DateTimeKind.Local).AddTicks(2164),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 9, 20, 5, 5, 882, DateTimeKind.Local).AddTicks(2895));
        }
    }
}
