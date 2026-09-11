using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Evaluaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    IdEvaluacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEstudiante = table.Column<int>(type: "int", nullable: false),
                    IdAsignacionDocente = table.Column<int>(type: "int", nullable: false),
                    IdSubPeriodo = table.Column<int>(type: "int", nullable: false),
                    Asistencia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tarea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cuaderno = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Participacion = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Proyecto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Exposicion = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Examen = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Activo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    IsDelete = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.IdEvaluacion);
                    table.ForeignKey(
                        name: "FK_Evaluations_Estudents_IdEstudiante",
                        column: x => x.IdEstudiante,
                        principalTable: "Estudents",
                        principalColumn: "IdEstudiante",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_SubPeriods_IdSubPeriodo",
                        column: x => x.IdSubPeriodo,
                        principalTable: "SubPeriods",
                        principalColumn: "IdSubPeriodo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_TeachingAssignment_IdAsignacionDocente",
                        column: x => x.IdAsignacionDocente,
                        principalTable: "TeachingAssignment",
                        principalColumn: "IdAsignacionDocente",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_IdAsignacionDocente",
                table: "Evaluations",
                column: "IdAsignacionDocente");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_IdEstudiante",
                table: "Evaluations",
                column: "IdEstudiante");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_IdSubPeriodo",
                table: "Evaluations",
                column: "IdSubPeriodo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evaluations");
        }
    }
}
