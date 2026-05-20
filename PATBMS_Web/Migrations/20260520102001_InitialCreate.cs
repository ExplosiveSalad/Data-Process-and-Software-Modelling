using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PATBMS_Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admissions",
                columns: table => new
                {
                    AdmissionID = table.Column<string>(type: "TEXT", nullable: false),
                    AdmissionDate = table.Column<string>(type: "TEXT", nullable: true),
                    AdmissionType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admissions", x => x.AdmissionID);
                });

            migrationBuilder.CreateTable(
                name: "BedAllocations",
                columns: table => new
                {
                    AllocationID = table.Column<string>(type: "TEXT", nullable: false),
                    AllocationDate = table.Column<string>(type: "TEXT", nullable: true),
                    ApprovedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BedAllocations", x => x.AllocationID);
                });

            migrationBuilder.CreateTable(
                name: "Beds",
                columns: table => new
                {
                    BedID = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    BedType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beds", x => x.BedID);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosisRecords",
                columns: table => new
                {
                    DiagnosisID = table.Column<string>(type: "TEXT", nullable: false),
                    Diagnosis = table.Column<string>(type: "TEXT", nullable: true),
                    TreatmentPlan = table.Column<string>(type: "TEXT", nullable: true),
                    DateRecorded = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosisRecords", x => x.DiagnosisID);
                });

            migrationBuilder.CreateTable(
                name: "Discharges",
                columns: table => new
                {
                    DischargeID = table.Column<string>(type: "TEXT", nullable: false),
                    DischargeDate = table.Column<string>(type: "TEXT", nullable: true),
                    DischargeReason = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discharges", x => x.DischargeID);
                });

            migrationBuilder.CreateTable(
                name: "HandoverRecords",
                columns: table => new
                {
                    HandoverID = table.Column<string>(type: "TEXT", nullable: false),
                    ClinicalStatus = table.Column<string>(type: "TEXT", nullable: true),
                    OutstandingTasks = table.Column<string>(type: "TEXT", nullable: true),
                    SpecialNeeds = table.Column<string>(type: "TEXT", nullable: true),
                    SendingDepartment = table.Column<string>(type: "TEXT", nullable: true),
                    ReceivingDepartment = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HandoverRecords", x => x.HandoverID);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationID = table.Column<string>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    DateSent = table.Column<string>(type: "TEXT", nullable: true),
                    IsAcknowledged = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationID);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientID = table.Column<string>(type: "TEXT", nullable: false),
                    NHINumber = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    DateOfBirth = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientID);
                });

            migrationBuilder.CreateTable(
                name: "TriageAssessments",
                columns: table => new
                {
                    TriageID = table.Column<string>(type: "TEXT", nullable: false),
                    ATSCategory = table.Column<int>(type: "INTEGER", nullable: false),
                    Symptoms = table.Column<string>(type: "TEXT", nullable: true),
                    Vitals = table.Column<string>(type: "TEXT", nullable: true),
                    AssessmentDate = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriageAssessments", x => x.TriageID);
                });

            migrationBuilder.CreateTable(
                name: "Wards",
                columns: table => new
                {
                    WardID = table.Column<string>(type: "TEXT", nullable: false),
                    WardName = table.Column<string>(type: "TEXT", nullable: true),
                    Specialty = table.Column<string>(type: "TEXT", nullable: true),
                    TotalBeds = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wards", x => x.WardID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admissions");

            migrationBuilder.DropTable(
                name: "BedAllocations");

            migrationBuilder.DropTable(
                name: "Beds");

            migrationBuilder.DropTable(
                name: "DiagnosisRecords");

            migrationBuilder.DropTable(
                name: "Discharges");

            migrationBuilder.DropTable(
                name: "HandoverRecords");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "TriageAssessments");

            migrationBuilder.DropTable(
                name: "Wards");
        }
    }
}
