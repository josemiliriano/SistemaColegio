using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class EntidadesFinales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodigoProfesor",
                table: "Professors",
                newName: "Cedula");

            migrationBuilder.AddColumn<string>(
                name: "Activo",
                table: "Persons",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProfesorIdProfesor",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    IdCurso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    IsDelete = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.IdCurso);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    IdMateria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    IsDelete = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.IdMateria);
                });

            migrationBuilder.CreateTable(
                name: "CoursePeriods",
                columns: table => new
                {
                    IdCursoPeriodo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCurso = table.Column<int>(type: "int", nullable: false),
                    IdPeriodo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePeriods", x => x.IdCursoPeriodo);
                    table.ForeignKey(
                        name: "FK_CoursePeriods_Courses_IdCurso",
                        column: x => x.IdCurso,
                        principalTable: "Courses",
                        principalColumn: "IdCurso",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoursePeriods_Periods_IdPeriodo",
                        column: x => x.IdPeriodo,
                        principalTable: "Periods",
                        principalColumn: "IdPeriodo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    IdSeccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCurso = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CupoCapacidadMaximo = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    IsDelete = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    CourseIdCurso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.IdSeccion);
                    table.ForeignKey(
                        name: "FK_Session_Courses_CourseIdCurso",
                        column: x => x.CourseIdCurso,
                        principalTable: "Courses",
                        principalColumn: "IdCurso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorSubjects",
                columns: table => new
                {
                    IdProfesorMateria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProfesor = table.Column<int>(type: "int", nullable: false),
                    IdMateria = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    IsDelete = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    SubjectIdMateria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorSubjects", x => x.IdProfesorMateria);
                    table.ForeignKey(
                        name: "FK_ProfessorSubjects_Professors_IdProfesor",
                        column: x => x.IdProfesor,
                        principalTable: "Professors",
                        principalColumn: "IdProfesor",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfessorSubjects_Subjects_SubjectIdMateria",
                        column: x => x.SubjectIdMateria,
                        principalTable: "Subjects",
                        principalColumn: "IdMateria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdPersona",
                table: "Users",
                column: "IdPersona",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ProfesorIdProfesor",
                table: "Persons",
                column: "ProfesorIdProfesor");

            migrationBuilder.CreateIndex(
                name: "IX_Estudents_IdPersona",
                table: "Estudents",
                column: "IdPersona",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoursePeriods_IdCurso_IdPeriodo",
                table: "CoursePeriods",
                columns: new[] { "IdCurso", "IdPeriodo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoursePeriods_IdPeriodo",
                table: "CoursePeriods",
                column: "IdPeriodo");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorSubjects_IdProfesor_IdMateria",
                table: "ProfessorSubjects",
                columns: new[] { "IdProfesor", "IdMateria" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorSubjects_SubjectIdMateria",
                table: "ProfessorSubjects",
                column: "SubjectIdMateria");

            migrationBuilder.CreateIndex(
                name: "IX_Session_CourseIdCurso",
                table: "Session",
                column: "CourseIdCurso");

            migrationBuilder.AddForeignKey(
                name: "FK_Estudents_Persons_IdPersona",
                table: "Estudents",
                column: "IdPersona",
                principalTable: "Persons",
                principalColumn: "IdPersona",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Professors_ProfesorIdProfesor",
                table: "Persons",
                column: "ProfesorIdProfesor",
                principalTable: "Professors",
                principalColumn: "IdProfesor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Persons_IdPersona",
                table: "Users",
                column: "IdPersona",
                principalTable: "Persons",
                principalColumn: "IdPersona",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudents_Persons_IdPersona",
                table: "Estudents");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Professors_ProfesorIdProfesor",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Persons_IdPersona",
                table: "Users");

            migrationBuilder.DropTable(
                name: "CoursePeriods");

            migrationBuilder.DropTable(
                name: "ProfessorSubjects");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Users_IdPersona",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Persons_ProfesorIdProfesor",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Estudents_IdPersona",
                table: "Estudents");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "ProfesorIdProfesor",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "Cedula",
                table: "Professors",
                newName: "CodigoProfesor");
        }
    }
}
