using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClinicaFisioterapiaApi.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class People : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "clinics",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "Neighborhood",
                table: "clinics",
                newName: "neighborhood");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "clinics",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "clinics",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "clinics",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "clinics",
                newName: "zip_code");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "clinics",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "clinics",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "clinics",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "ClinicId",
                table: "clinics",
                newName: "clinic_id");

            migrationBuilder.CreateTable(
                name: "people",
                columns: table => new
                {
                    person_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    full_name = table.Column<string>(type: "text", nullable: false),
                    cpf = table.Column<string>(type: "text", nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    neighborhood = table.Column<string>(type: "text", nullable: true),
                    city = table.Column<string>(type: "text", nullable: true),
                    state = table.Column<string>(type: "text", nullable: true),
                    zip_code = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_people", x => x.person_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "people");

            migrationBuilder.RenameColumn(
                name: "state",
                table: "clinics",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "neighborhood",
                table: "clinics",
                newName: "Neighborhood");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "clinics",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "clinics",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "clinics",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "zip_code",
                table: "clinics",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "clinics",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "clinics",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "clinics",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "clinic_id",
                table: "clinics",
                newName: "ClinicId");
        }
    }
}
