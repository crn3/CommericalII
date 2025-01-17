using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCApp.Migrations
{
    /// <inheritdoc />
    public partial class ppsforeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientPPS",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientPPS",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PatientPPS",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "PPS",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PPS",
                table: "Appointments",
                column: "PPS");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PPS",
                table: "Appointments",
                column: "PPS",
                principalTable: "Patients",
                principalColumn: "PPS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PPS",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PPS",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "PPS",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientPPS",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientPPS",
                table: "Appointments",
                column: "PatientPPS");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientPPS",
                table: "Appointments",
                column: "PatientPPS",
                principalTable: "Patients",
                principalColumn: "PPS");
        }
    }
}
