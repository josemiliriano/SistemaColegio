using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class FixSessionCourseRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_Courses_CourseIdCurso",
                table: "Session");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionPeriod_Session_IdSeccion",
                table: "SessionPeriod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Session",
                table: "Session");

            migrationBuilder.DropIndex(
                name: "IX_Session_CourseIdCurso",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "CourseIdCurso",
                table: "Session");

            migrationBuilder.RenameTable(
                name: "Session",
                newName: "Sessions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions",
                column: "IdSeccion");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_IdCurso",
                table: "Sessions",
                column: "IdCurso");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionPeriod_Sessions_IdSeccion",
                table: "SessionPeriod",
                column: "IdSeccion",
                principalTable: "Sessions",
                principalColumn: "IdSeccion",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Courses_IdCurso",
                table: "Sessions",
                column: "IdCurso",
                principalTable: "Courses",
                principalColumn: "IdCurso",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionPeriod_Sessions_IdSeccion",
                table: "SessionPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Courses_IdCurso",
                table: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_IdCurso",
                table: "Sessions");

            migrationBuilder.RenameTable(
                name: "Sessions",
                newName: "Session");

            migrationBuilder.AddColumn<int>(
                name: "CourseIdCurso",
                table: "Session",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Session",
                table: "Session",
                column: "IdSeccion");

            migrationBuilder.CreateIndex(
                name: "IX_Session_CourseIdCurso",
                table: "Session",
                column: "CourseIdCurso");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Courses_CourseIdCurso",
                table: "Session",
                column: "CourseIdCurso",
                principalTable: "Courses",
                principalColumn: "IdCurso",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionPeriod_Session_IdSeccion",
                table: "SessionPeriod",
                column: "IdSeccion",
                principalTable: "Session",
                principalColumn: "IdSeccion",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
