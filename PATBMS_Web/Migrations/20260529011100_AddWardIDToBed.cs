using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PATBMS_Web.Migrations
{
    /// <inheritdoc />
    public partial class AddWardIDToBed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WardID",
                table: "Beds",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WardID",
                table: "Beds");
        }
    }
}
