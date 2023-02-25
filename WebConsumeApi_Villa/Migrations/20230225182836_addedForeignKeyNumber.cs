using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebConsumeApi_Villa.Migrations
{
    /// <inheritdoc />
    public partial class addedForeignKeyNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaID",
                table: "villaNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 2, 25, 23, 58, 36, 61, DateTimeKind.Local).AddTicks(8598));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2023, 2, 25, 23, 58, 36, 61, DateTimeKind.Local).AddTicks(8611));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2023, 2, 25, 23, 58, 36, 61, DateTimeKind.Local).AddTicks(8613));

            migrationBuilder.CreateIndex(
                name: "IX_villaNumbers_VillaID",
                table: "villaNumbers",
                column: "VillaID");

            migrationBuilder.AddForeignKey(
                name: "FK_villaNumbers_villas_VillaID",
                table: "villaNumbers",
                column: "VillaID",
                principalTable: "villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_villaNumbers_villas_VillaID",
                table: "villaNumbers");

            migrationBuilder.DropIndex(
                name: "IX_villaNumbers_VillaID",
                table: "villaNumbers");

            migrationBuilder.DropColumn(
                name: "VillaID",
                table: "villaNumbers");

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 2, 25, 22, 30, 42, 911, DateTimeKind.Local).AddTicks(7948));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2023, 2, 25, 22, 30, 42, 911, DateTimeKind.Local).AddTicks(7959));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2023, 2, 25, 22, 30, 42, 911, DateTimeKind.Local).AddTicks(7960));
        }
    }
}
