using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Professors_ProfesorIdProfesor",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_ProfesorIdProfesor",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "ProfesorIdProfesor",
                table: "Persons");

            migrationBuilder.AddColumn<int>(
                name: "IdRol",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreRol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    IsDelete = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRol);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdRol",
                table: "Users",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Professors_IdPersona",
                table: "Professors",
                column: "IdPersona",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Professors_Persons_IdPersona",
                table: "Professors",
                column: "IdPersona",
                principalTable: "Persons",
                principalColumn: "IdPersona",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_IdRol",
                table: "Users",
                column: "IdRol",
                principalTable: "Roles",
                principalColumn: "IdRol",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professors_Persons_IdPersona",
                table: "Professors");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_IdRol",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Users_IdRol",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Professors_IdPersona",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "IdRol",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "ProfesorIdProfesor",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ProfesorIdProfesor",
                table: "Persons",
                column: "ProfesorIdProfesor");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Professors_ProfesorIdProfesor",
                table: "Persons",
                column: "ProfesorIdProfesor",
                principalTable: "Professors",
                principalColumn: "IdProfesor",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
