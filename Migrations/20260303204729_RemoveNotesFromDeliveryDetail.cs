using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnTech_PBS_UniformManagement.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNotesFromDeliveryDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "DeliveryDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "DeliveryDetails",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
