using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bagel_sales_control.Migrations
{
    public partial class ModifyTablesFieldsControls1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionModify",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TransactionModify",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "TransactionModify",
                table: "Product");

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionModificationDate",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionModificationDate",
                table: "Sales",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionModificationDate",
                table: "Product",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionModificationDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TransactionModificationDate",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "TransactionModificationDate",
                table: "Product");

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionModify",
                table: "User",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionModify",
                table: "Sales",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionModify",
                table: "Product",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
