using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductAPI.Data.Migrations
{
    public partial class update_viewcount_col : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1b60fd43-a1b5-4214-9ccc-d239f0f4c97f"),
                column: "CreatedDate",
                value: new DateTime(2021, 11, 24, 2, 41, 30, 919, DateTimeKind.Local).AddTicks(8811));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1b60fd43-a1b5-4214-9ccc-d239f0f4c97f"),
                column: "CreatedDate",
                value: new DateTime(2021, 11, 23, 22, 43, 39, 853, DateTimeKind.Local).AddTicks(148));
        }
    }
}
