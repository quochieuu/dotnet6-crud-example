using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductAPI.Data.Migrations
{
    public partial class updateseeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1b60fd43-a1b5-4214-9ccc-d239f0f4c97f"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedDate", "Name", "Price", "Status", "UrlImage" },
                values: new object[] { new Guid("1b60fd43-a1b5-4214-9ccc-d239f0f4c97f"), new Guid("1b60fd43-a1b5-4214-9ccc-d239f0f4c97b"), "New fashion 2021", new DateTime(2021, 11, 23, 22, 43, 39, 853, DateTimeKind.Local).AddTicks(148), "Nike", 120000m, true, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1b60fd43-a1b5-4214-9ccc-d239f0f4c97f"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Name", "ParentId", "Status" },
                values: new object[] { new Guid("1b60fd43-a1b5-4214-9ccc-d239f0f4c97f"), new DateTime(2021, 11, 23, 21, 57, 3, 794, DateTimeKind.Local).AddTicks(2429), "Nike", null, true });
        }
    }
}
