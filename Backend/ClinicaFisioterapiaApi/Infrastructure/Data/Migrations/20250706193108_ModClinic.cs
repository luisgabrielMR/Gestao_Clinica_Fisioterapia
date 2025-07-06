using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaFisioterapiaApi.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModClinic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Clinics",
                table: "Clinics");

            migrationBuilder.RenameTable(
                name: "Clinics",
                newName: "clinics");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clinics",
                table: "clinics",
                column: "ClinicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_clinics",
                table: "clinics");

            migrationBuilder.RenameTable(
                name: "clinics",
                newName: "Clinics");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clinics",
                table: "Clinics",
                column: "ClinicId");
        }
    }
}
