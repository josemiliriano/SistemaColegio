using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Notas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Months",
                columns: table => new
                {
                    IdMes = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    IsDelete = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Months", x => x.IdMes);
                });

            migrationBuilder.CreateTable(
                name: "SubPeriods",
                columns: table => new
                {
                    IdSubPeriodo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    IsDelete = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubPeriods", x => x.IdSubPeriodo);
                });

            migrationBuilder.CreateTable(
                name: "AcademicSubPeriods",
                columns: table => new
                {
                    IdPeriodoAcademico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPeriodo = table.Column<int>(type: "int", nullable: false),
                    IdSubPeriodo = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: false),
                    Activo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    IsDelete = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSubPeriods", x => x.IdPeriodoAcademico);
                    table.ForeignKey(
                        name: "FK_AcademicSubPeriods_Periods_IdPeriodo",
                        column: x => x.IdPeriodo,
                        principalTable: "Periods",
                        principalColumn: "IdPeriodo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AcademicSubPeriods_SubPeriods_IdSubPeriodo",
                        column: x => x.IdSubPeriodo,
                        principalTable: "SubPeriods",
                        principalColumn: "IdSubPeriodo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AcademicMonths",
                columns: table => new
                {
                    IdMesAcademico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPeriodoAcademico = table.Column<int>(type: "int", nullable: false),
                    IdMes = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    IsDelete = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicMonths", x => x.IdMesAcademico);
                    table.ForeignKey(
                        name: "FK_AcademicMonths_AcademicSubPeriods_IdPeriodoAcademico",
                        column: x => x.IdPeriodoAcademico,
                        principalTable: "AcademicSubPeriods",
                        principalColumn: "IdPeriodoAcademico",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AcademicMonths_Months_IdMes",
                        column: x => x.IdMes,
                        principalTable: "Months",
                        principalColumn: "IdMes",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicMonths_IdMes",
                table: "AcademicMonths",
                column: "IdMes");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicMonths_IdPeriodoAcademico",
                table: "AcademicMonths",
                column: "IdPeriodoAcademico");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSubPeriods_IdPeriodo",
                table: "AcademicSubPeriods",
                column: "IdPeriodo");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSubPeriods_IdSubPeriodo",
                table: "AcademicSubPeriods",
                column: "IdSubPeriodo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicMonths");

            migrationBuilder.DropTable(
                name: "AcademicSubPeriods");

            migrationBuilder.DropTable(
                name: "Months");

            migrationBuilder.DropTable(
                name: "SubPeriods");
        }
    }
}
