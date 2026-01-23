using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnTech_PBS_UniformManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddUniformManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeHealthAreas_AspNetUsers_EmployeeId",
                table: "EmployeeHealthAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeHealthAreas_HealthAreas_HealthAreaId",
                table: "EmployeeHealthAreas");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "EmployeeHealthAreas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UniformDeliveries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HealthAreaId = table.Column<int>(type: "int", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Sin entrega"),
                    Observations = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DaysUntilNextDelivery = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniformDeliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UniformDeliveries_EmployeeHealthAreas_EmployeeId_HealthAreaId",
                        columns: x => new { x.EmployeeId, x.HealthAreaId },
                        principalTable: "EmployeeHealthAreas",
                        principalColumns: new[] { "EmployeeId", "HealthAreaId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniformDeliveryId = table.Column<int>(type: "int", nullable: false),
                    GarmentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Size = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Notes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryDetails_UniformDeliveries_UniformDeliveryId",
                        column: x => x.UniformDeliveryId,
                        principalTable: "UniformDeliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHealthAreas_Position",
                table: "EmployeeHealthAreas",
                column: "Position");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDetails_GarmentType",
                table: "DeliveryDetails",
                column: "GarmentType");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDetails_UniformDeliveryId",
                table: "DeliveryDetails",
                column: "UniformDeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_UniformDeliveries_DeliveryDate",
                table: "UniformDeliveries",
                column: "DeliveryDate");

            migrationBuilder.CreateIndex(
                name: "IX_UniformDeliveries_EmployeeId_HealthAreaId",
                table: "UniformDeliveries",
                columns: new[] { "EmployeeId", "HealthAreaId" });

            migrationBuilder.CreateIndex(
                name: "IX_UniformDeliveries_Status",
                table: "UniformDeliveries",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeHealthAreas_AspNetUsers_EmployeeId",
                table: "EmployeeHealthAreas",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeHealthAreas_HealthAreas_HealthAreaId",
                table: "EmployeeHealthAreas",
                column: "HealthAreaId",
                principalTable: "HealthAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeHealthAreas_AspNetUsers_EmployeeId",
                table: "EmployeeHealthAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeHealthAreas_HealthAreas_HealthAreaId",
                table: "EmployeeHealthAreas");

            migrationBuilder.DropTable(
                name: "DeliveryDetails");

            migrationBuilder.DropTable(
                name: "UniformDeliveries");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeHealthAreas_Position",
                table: "EmployeeHealthAreas");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "EmployeeHealthAreas");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeHealthAreas_AspNetUsers_EmployeeId",
                table: "EmployeeHealthAreas",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeHealthAreas_HealthAreas_HealthAreaId",
                table: "EmployeeHealthAreas",
                column: "HealthAreaId",
                principalTable: "HealthAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
