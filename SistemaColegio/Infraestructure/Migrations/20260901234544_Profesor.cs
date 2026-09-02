using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Profesor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professors_Persons_IdPersona",
                table: "Professors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorSubjects_Subjects_SubjectIdMateria",
                table: "ProfessorSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_IdRol",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorSubjects_SubjectIdMateria",
                table: "ProfessorSubjects");

            migrationBuilder.DropColumn(
                name: "SubjectIdMateria",
                table: "ProfessorSubjects");

            migrationBuilder.CreateTable(
                name: "Classroom",
                columns: table => new
                {
                    IdAula = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacidad = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    IsDelete = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classroom", x => x.IdAula);
                });

            migrationBuilder.CreateTable(
                name: "CourseSubjects",
                columns: table => new
                {
                    IdCursoMateria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCurso = table.Column<int>(type: "int", nullable: false),
                    IdMateria = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    IsDelete = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSubjects", x => x.IdCursoMateria);
                    table.ForeignKey(
                        name: "FK_CourseSubjects_Courses_IdCurso",
                        column: x => x.IdCurso,
                        principalTable: "Courses",
                        principalColumn: "IdCurso",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseSubjects_Subjects_IdMateria",
                        column: x => x.IdMateria,
                        principalTable: "Subjects",
                        principalColumn: "IdMateria",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SessionPeriod",
                columns: table => new
                {
                    IdSessionPeriod = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSeccion = table.Column<int>(type: "int", nullable: false),
                    IdPeriodo = table.Column<int>(type: "int", nullable: false),
                    IdAula = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionPeriod", x => x.IdSessionPeriod);
                    table.ForeignKey(
                        name: "FK_SessionPeriod_Classroom_IdAula",
                        column: x => x.IdAula,
                        principalTable: "Classroom",
                        principalColumn: "IdAula",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionPeriod_Periods_IdPeriodo",
                        column: x => x.IdPeriodo,
                        principalTable: "Periods",
                        principalColumn: "IdPeriodo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionPeriod_Session_IdSeccion",
                        column: x => x.IdSeccion,
                        principalTable: "Session",
                        principalColumn: "IdSeccion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeachingAssignment",
                columns: table => new
                {
                    IdAsignacionDocente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProfesorMateria = table.Column<int>(type: "int", nullable: false),
                    IdSessionPeriod = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    IsDelete = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingAssignment", x => x.IdAsignacionDocente);
                    table.ForeignKey(
                        name: "FK_TeachingAssignment_ProfessorSubjects_IdProfesorMateria",
                        column: x => x.IdProfesorMateria,
                        principalTable: "ProfessorSubjects",
                        principalColumn: "IdProfesorMateria",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeachingAssignment_SessionPeriod_IdSessionPeriod",
                        column: x => x.IdSessionPeriod,
                        principalTable: "SessionPeriod",
                        principalColumn: "IdSessionPeriod",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorSubjects_IdMateria",
                table: "ProfessorSubjects",
                column: "IdMateria");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSubjects_IdCurso_IdMateria",
                table: "CourseSubjects",
                columns: new[] { "IdCurso", "IdMateria" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseSubjects_IdMateria",
                table: "CourseSubjects",
                column: "IdMateria");

            migrationBuilder.CreateIndex(
                name: "IX_SessionPeriod_IdAula",
                table: "SessionPeriod",
                column: "IdAula");

            migrationBuilder.CreateIndex(
                name: "IX_SessionPeriod_IdPeriodo",
                table: "SessionPeriod",
                column: "IdPeriodo");

            migrationBuilder.CreateIndex(
                name: "IX_SessionPeriod_IdSeccion",
                table: "SessionPeriod",
                column: "IdSeccion");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssignment_IdProfesorMateria_IdSessionPeriod",
                table: "TeachingAssignment",
                columns: new[] { "IdProfesorMateria", "IdSessionPeriod" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssignment_IdSessionPeriod",
                table: "TeachingAssignment",
                column: "IdSessionPeriod");

            migrationBuilder.AddForeignKey(
                name: "FK_Professors_Persons_IdPersona",
                table: "Professors",
                column: "IdPersona",
                principalTable: "Persons",
                principalColumn: "IdPersona",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorSubjects_Subjects_IdMateria",
                table: "ProfessorSubjects",
                column: "IdMateria",
                principalTable: "Subjects",
                principalColumn: "IdMateria",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_IdRol",
                table: "Users",
                column: "IdRol",
                principalTable: "Roles",
                principalColumn: "IdRol",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professors_Persons_IdPersona",
                table: "Professors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorSubjects_Subjects_IdMateria",
                table: "ProfessorSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_IdRol",
                table: "Users");

            migrationBuilder.DropTable(
                name: "CourseSubjects");

            migrationBuilder.DropTable(
                name: "TeachingAssignment");

            migrationBuilder.DropTable(
                name: "SessionPeriod");

            migrationBuilder.DropTable(
                name: "Classroom");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorSubjects_IdMateria",
                table: "ProfessorSubjects");

            migrationBuilder.AddColumn<int>(
                name: "SubjectIdMateria",
                table: "ProfessorSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorSubjects_SubjectIdMateria",
                table: "ProfessorSubjects",
                column: "SubjectIdMateria");

            migrationBuilder.AddForeignKey(
                name: "FK_Professors_Persons_IdPersona",
                table: "Professors",
                column: "IdPersona",
                principalTable: "Persons",
                principalColumn: "IdPersona",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorSubjects_Subjects_SubjectIdMateria",
                table: "ProfessorSubjects",
                column: "SubjectIdMateria",
                principalTable: "Subjects",
                principalColumn: "IdMateria",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_IdRol",
                table: "Users",
                column: "IdRol",
                principalTable: "Roles",
                principalColumn: "IdRol",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
