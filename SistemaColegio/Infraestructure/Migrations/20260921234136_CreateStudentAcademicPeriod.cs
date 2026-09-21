using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateStudentAcademicPeriod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstudentAcademicPeriods",
                columns: table => new
                {
                    IdStudentAcademicPeriod = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEstudiante = table.Column<int>(type: "int", nullable: false),
                    IdPeriodo = table.Column<int>(type: "int", nullable: false),
                    IdSessionPeriod = table.Column<int>(type: "int", nullable: false),
                    Resultado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    IsDelete = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstudentAcademicPeriods", x => x.IdStudentAcademicPeriod);
                    table.ForeignKey(
                        name: "FK_EstudentAcademicPeriods_Estudents_IdEstudiante",
                        column: x => x.IdEstudiante,
                        principalTable: "Estudents",
                        principalColumn: "IdEstudiante",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EstudentAcademicPeriods_Periods_IdPeriodo",
                        column: x => x.IdPeriodo,
                        principalTable: "Periods",
                        principalColumn: "IdPeriodo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EstudentAcademicPeriods_SessionPeriod_IdSessionPeriod",
                        column: x => x.IdSessionPeriod,
                        principalTable: "SessionPeriod",
                        principalColumn: "IdSessionPeriod",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstudentAcademicPeriods_IdEstudiante_IdPeriodo",
                table: "EstudentAcademicPeriods",
                columns: new[] { "IdEstudiante", "IdPeriodo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EstudentAcademicPeriods_IdPeriodo",
                table: "EstudentAcademicPeriods",
                column: "IdPeriodo");

            migrationBuilder.CreateIndex(
                name: "IX_EstudentAcademicPeriods_IdSessionPeriod",
                table: "EstudentAcademicPeriods",
                column: "IdSessionPeriod");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstudentAcademicPeriods");
        }
    }
}
