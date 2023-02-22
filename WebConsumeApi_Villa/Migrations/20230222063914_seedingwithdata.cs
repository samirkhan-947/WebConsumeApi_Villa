using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebConsumeApi_Villa.Migrations
{
    /// <inheritdoc />
    public partial class seedingwithdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "villas",
                columns: new[] { "Id", "Amenity", "CreateDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2023, 2, 22, 12, 9, 14, 488, DateTimeKind.Local).AddTicks(8591), "this is the banglaure villa with beautyful house and decent price ", "", "Banglore villa", 5, 200.0, 550, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "", new DateTime(2023, 2, 22, 12, 9, 14, 488, DateTimeKind.Local).AddTicks(8602), "this is the pune villa with beautyful house and decent price ", "", "pune villa", 4, 300.0, 450, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "", new DateTime(2023, 2, 22, 12, 9, 14, 488, DateTimeKind.Local).AddTicks(8605), "this is the pune villa with beautyful house and decent price ", "", "patna villa", 3, 600.0, 850, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
