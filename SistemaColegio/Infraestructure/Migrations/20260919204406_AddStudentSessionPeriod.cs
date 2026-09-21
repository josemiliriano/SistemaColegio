using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentSessionPeriod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudents_SessionPeriod_SessionPeriodIdSessionPeriod",
                table: "Estudents");

            migrationBuilder.DropIndex(
                name: "IX_Estudents_SessionPeriodIdSessionPeriod",
                table: "Estudents");

            migrationBuilder.DropColumn(
                name: "SessionPeriodIdSessionPeriod",
                table: "Estudents");

            migrationBuilder.CreateIndex(
                name: "IX_Estudents_IdSessionPeriod",
                table: "Estudents",
                column: "IdSessionPeriod");

            migrationBuilder.AddForeignKey(
                name: "FK_Estudents_SessionPeriod_IdSessionPeriod",
                table: "Estudents",
                column: "IdSessionPeriod",
                principalTable: "SessionPeriod",
                principalColumn: "IdSessionPeriod",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudents_SessionPeriod_IdSessionPeriod",
                table: "Estudents");

            migrationBuilder.DropIndex(
                name: "IX_Estudents_IdSessionPeriod",
                table: "Estudents");

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
                name: "FK_Estudents_SessionPeriod_SessionPeriodIdSessionPeriod",
                table: "Estudents",
                column: "SessionPeriodIdSessionPeriod",
                principalTable: "SessionPeriod",
                principalColumn: "IdSessionPeriod",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
