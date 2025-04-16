using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatheusR.Motok.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Create_Table_Motorcycles_2024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "motorcycles_2024",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MotorcycleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_motorcycles_2024", x => x.Id);
                    table.ForeignKey(
                        name: "FK_motorcycles_2024_motorcycles_MotorcycleId",
                        column: x => x.MotorcycleId,
                        principalTable: "motorcycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_motorcycles_2024_MotorcycleId",
                table: "motorcycles_2024",
                column: "MotorcycleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "motorcycles_2024");
        }
    }
}
