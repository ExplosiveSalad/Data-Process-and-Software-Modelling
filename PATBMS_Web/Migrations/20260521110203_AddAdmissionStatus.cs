using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PATBMS_Web.Migrations
{
    /// <inheritdoc />
    public partial class AddAdmissionStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdmissionStatus",
                table: "Patients",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdmissionStatus",
                table: "Patients");
        }
    }
}
