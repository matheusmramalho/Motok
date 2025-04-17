using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatheusR.Motok.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Update_Table_Rent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "expected_final_date",
                table: "rents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "is_rent_active",
                table: "rents",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "expected_final_date",
                table: "rents");

            migrationBuilder.DropColumn(
                name: "is_rent_active",
                table: "rents");
        }
    }
}
