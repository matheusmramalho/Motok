using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatheusR.Motok.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Create_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "deliveries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    licence_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    licence_type = table.Column<string>(type: "text", nullable: false),
                    licence_image_path = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deliveries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "motorcycles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    identifier = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    model = table.Column<string>(type: "text", nullable: false),
                    licence_plate = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_motorcycles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    initial_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    final_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    expected_price = table.Column<decimal>(type: "numeric", nullable: false),
                    final_price = table.Column<decimal>(type: "numeric", nullable: true),
                    has_fine = table.Column<bool>(type: "boolean", nullable: true),
                    rent_plan = table.Column<string>(type: "text", nullable: false),
                    DeliveryId = table.Column<Guid>(type: "uuid", nullable: false),
                    MotorcycleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rents_deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rents_motorcycles_MotorcycleId",
                        column: x => x.MotorcycleId,
                        principalTable: "motorcycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_deliveries_cnpj",
                table: "deliveries",
                column: "cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_deliveries_licence_number",
                table: "deliveries",
                column: "licence_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rents_DeliveryId",
                table: "rents",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_rents_MotorcycleId",
                table: "rents",
                column: "MotorcycleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rents");

            migrationBuilder.DropTable(
                name: "deliveries");

            migrationBuilder.DropTable(
                name: "motorcycles");
        }
    }
}
