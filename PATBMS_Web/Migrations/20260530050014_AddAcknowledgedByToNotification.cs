using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PATBMS_Web.Migrations
{
    /// <inheritdoc />
    public partial class AddAcknowledgedByToNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AcknowledgedBy",
                table: "Notifications",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcknowledgedBy",
                table: "Notifications");
        }
    }
}
