using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class PeriodCouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Activo",
                table: "CoursePeriods",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IsDelete",
                table: "CoursePeriods",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "CoursePeriods");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "CoursePeriods");
        }
    }
}
