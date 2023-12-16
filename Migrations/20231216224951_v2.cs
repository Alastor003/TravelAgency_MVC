using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency_MVC.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "users",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "cityName",
                table: "City",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.UpdateData(
                table: "flights",
                keyColumn: "id",
                keyValue: 1,
                column: "date",
                value: new DateTime(2024, 1, 15, 19, 49, 51, 442, DateTimeKind.Local).AddTicks(5212));

            migrationBuilder.UpdateData(
                table: "flights",
                keyColumn: "id",
                keyValue: 2,
                column: "date",
                value: new DateTime(2024, 1, 30, 19, 49, 51, 442, DateTimeKind.Local).AddTicks(5228));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "idUser",
                keyValue: 1,
                column: "image",
                value: null);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "idUser",
                keyValue: 2,
                column: "image",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "users");

            migrationBuilder.AlterColumn<string>(
                name: "cityName",
                table: "City",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "flights",
                keyColumn: "id",
                keyValue: 1,
                column: "date",
                value: new DateTime(2024, 1, 2, 15, 7, 37, 356, DateTimeKind.Local).AddTicks(3186));

            migrationBuilder.UpdateData(
                table: "flights",
                keyColumn: "id",
                keyValue: 2,
                column: "date",
                value: new DateTime(2024, 1, 17, 15, 7, 37, 356, DateTimeKind.Local).AddTicks(3200));
        }
    }
}
