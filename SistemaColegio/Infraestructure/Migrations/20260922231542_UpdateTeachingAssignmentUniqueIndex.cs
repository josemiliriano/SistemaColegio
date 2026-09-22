using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTeachingAssignmentUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeachingAssignment_IdProfesorMateria_IdSessionPeriod",
                table: "TeachingAssignment");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssignment_IdProfesorMateria_IdSessionPeriod",
                table: "TeachingAssignment",
                columns: new[] { "IdProfesorMateria", "IdSessionPeriod" },
                unique: true,
                filter: "[IsDelete] = '0'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeachingAssignment_IdProfesorMateria_IdSessionPeriod",
                table: "TeachingAssignment");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssignment_IdProfesorMateria_IdSessionPeriod",
                table: "TeachingAssignment",
                columns: new[] { "IdProfesorMateria", "IdSessionPeriod" },
                unique: true);
        }
    }
}
