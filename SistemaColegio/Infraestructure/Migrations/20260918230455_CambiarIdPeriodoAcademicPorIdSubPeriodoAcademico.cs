using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class CambiarIdPeriodoAcademicPorIdSubPeriodoAcademico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicMonths_AcademicSubPeriods_IdPeriodoAcademico",
                table: "AcademicMonths");

            migrationBuilder.RenameColumn(
                name: "IdPeriodoAcademico",
                table: "AcademicMonths",
                newName: "IdSubPeriodoAcademico");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicMonths_IdPeriodoAcademico",
                table: "AcademicMonths",
                newName: "IX_AcademicMonths_IdSubPeriodoAcademico");

            migrationBuilder.AddColumn<int>(
                name: "IdSessionPeriod",
                table: "Estudents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SessionPeriodIdSessionPeriod",
                table: "Estudents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Estudents_SessionPeriodIdSessionPeriod",
                table: "Estudents",
                column: "SessionPeriodIdSessionPeriod");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicMonths_AcademicSubPeriods_IdSubPeriodoAcademico",
                table: "AcademicMonths",
                column: "IdSubPeriodoAcademico",
                principalTable: "AcademicSubPeriods",
                principalColumn: "IdPeriodoAcademico",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Estudents_SessionPeriod_SessionPeriodIdSessionPeriod",
                table: "Estudents",
                column: "SessionPeriodIdSessionPeriod",
                principalTable: "SessionPeriod",
                principalColumn: "IdSessionPeriod",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicMonths_AcademicSubPeriods_IdSubPeriodoAcademico",
                table: "AcademicMonths");

            migrationBuilder.DropForeignKey(
                name: "FK_Estudents_SessionPeriod_SessionPeriodIdSessionPeriod",
                table: "Estudents");

            migrationBuilder.DropIndex(
                name: "IX_Estudents_SessionPeriodIdSessionPeriod",
                table: "Estudents");

            migrationBuilder.DropColumn(
                name: "IdSessionPeriod",
                table: "Estudents");

            migrationBuilder.DropColumn(
                name: "SessionPeriodIdSessionPeriod",
                table: "Estudents");

            migrationBuilder.RenameColumn(
                name: "IdSubPeriodoAcademico",
                table: "AcademicMonths",
                newName: "IdPeriodoAcademico");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicMonths_IdSubPeriodoAcademico",
                table: "AcademicMonths",
                newName: "IX_AcademicMonths_IdPeriodoAcademico");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicMonths_AcademicSubPeriods_IdPeriodoAcademico",
                table: "AcademicMonths",
                column: "IdPeriodoAcademico",
                principalTable: "AcademicSubPeriods",
                principalColumn: "IdPeriodoAcademico",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
